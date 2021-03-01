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
            Console.WriteLine(DeleteProjectById(context));
        }

        public static string DeleteProjectById(SoftUniContext context)
        {
            var employeesProject = context.EmployeesProjects
                .Where(x => x.ProjectId == 2);

            foreach (var employeeProject in employeesProject)
            {
                context.EmployeesProjects.Remove(employeeProject);
            }
            context.SaveChanges();

            var project = context.Projects
                .First(x => x.ProjectId == 2);

            context.Projects.Remove(project);
            context.SaveChanges();

            var projects = context.Projects
                .Select(x => x.Name)
                .Take(10)
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var projectName in projects)
            {
                sb.AppendLine(projectName);
            }
            return sb.ToString().Trim();
        }
    }
}
