using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using System;
using System.Linq;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();
            Console.WriteLine(RemoveTown(context));
        }

        public static string RemoveTown(SoftUniContext context)
        {
            int countDeleted = 0;

            var employees = context.Employees
                .Include(x => x.Address)
                .ThenInclude(x => x.Town)
                .Where(x => x.Address.Town.Name == "Seattle");

            foreach (var employee in employees)
            {
                employee.AddressId = null;
            }
            context.SaveChanges();

            var addresses = context.Addresses
                .Include(x => x.Town)
                .Where(x => x.Town.Name == "Seattle");

            foreach (var address in addresses)
            {
                context.Addresses.Remove(address);
                countDeleted++;
            }
            context.SaveChanges();

            var town = context.Towns
                .First(x => x.Name == "Seattle");

            context.Towns.Remove(town);
            context.SaveChanges();

            return $"{countDeleted} addresses in Seattle were deleted";
        }
    }
}
