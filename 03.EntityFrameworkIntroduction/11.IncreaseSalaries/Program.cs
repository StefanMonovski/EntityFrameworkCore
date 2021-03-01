using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();
            Console.WriteLine(IncreaseSalaries(context));
        }

        public static string IncreaseSalaries(SoftUniContext context)
        {
            List<string> departmentNames = new List<string>() { "Engineering", "Tool Design", "Marketing", "Information Services" };

            var employeesIncrease = context.Employees
                .Include(x => x.Departments)
                .Where(x => departmentNames.Contains(x.Department.Name))
                .ToList();

            employeesIncrease.ForEach(x => x.Salary *= 1.12M);
            context.SaveChanges();

            var employeesIncreased = context.Employees
                .Include(x => x.Departments)
                .Where(x => departmentNames.Contains(x.Department.Name))
                .Select(x => new { x.FirstName, x.LastName, x.Salary })
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var employeeIncreased in employeesIncreased)
            {
                sb.AppendLine($"{employeeIncreased.FirstName} {employeeIncreased.LastName} (${employeeIncreased.Salary:f2})");
            }
            return sb.ToString().Trim();
        }
    }
}
