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
            Console.WriteLine(GetEmployee147(context));
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            var employee = context.Employees
                .Include(x => x.EmployeesProjects)
                .ThenInclude(x => x.Project)
                .Select(x => new
                {
                    x.EmployeeId,
                    x.FirstName,
                    x.LastName,
                    x.JobTitle,
                    projects = x.EmployeesProjects
                    .Select(x => new { x.Project.Name })
                    .OrderBy(x => x.Name)
                    .ToList()
                })
                .Single(x => x.EmployeeId == 147);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
            foreach (var project in employee.projects)
            {
                sb.AppendLine($"{project.Name}");
            }
            return sb.ToString().Trim();
        }
    }
}