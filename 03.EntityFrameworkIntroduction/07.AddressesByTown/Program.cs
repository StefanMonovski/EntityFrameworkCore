using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
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
            Console.WriteLine(GetAddressesByTown(context));
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            var addresses = context.Addresses
                .Include(x => x.Town)
                .Include(x => x.Employees)
                .Select(x => new
                {
                    x.AddressText,
                    townName = x.Town.Name,
                    employeesCount = x.Employees.Count
                })
                .OrderByDescending(x => x.employeesCount)
                .ThenBy(x => x.townName)
                .ThenBy(x => x.AddressText)
                .Take(10)
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var address in addresses)
            {
                sb.AppendLine($"{address.AddressText}, {address.townName} - {address.employeesCount} employees");
            }
            return sb.ToString().Trim();
        }
    }
}
