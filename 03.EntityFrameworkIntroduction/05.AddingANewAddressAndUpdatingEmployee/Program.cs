using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();
            Console.WriteLine(AddNewAddressToEmployee(context));
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4,
            };
            context.Addresses.Add(address);
            context.SaveChanges();

            var updateEmployee = context.Employees
                .First(x => x.LastName == "Nakov");
            updateEmployee.AddressId = address.AddressId;
            context.SaveChanges();

            var employees = context.Employees
                .Select(x => new { x.AddressId, x.Address.AddressText })
                .OrderByDescending(x => x.AddressId)
                .Take(10)
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var employee in employees)
            {
                sb.AppendLine(employee.AddressText);
            }
            return sb.ToString().Trim();
        }
    }
}
