namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var projects = context.Projects.ToList()
                .Where(x => x.Tasks.Count > 0)
                .Select(x => new ExportProjectDto()
                {
                    TasksCount = x.Tasks.Count,
                    ProjectName = x.Name,
                    HasEndDateBool = x.DueDate.HasValue,
                    Tasks = x.Tasks.Select(x => new ExportTaskDto()
                    {
                        Name = x.Name,
                        Label = x.LabelType.ToString()
                    })
                    .OrderBy(x => x.Name)
                    .ToList()
                })
                .OrderByDescending(x => x.TasksCount)
                .ThenBy(x => x.ProjectName)
                .ToList();

            foreach (var project in projects)
            {
                if (project.HasEndDateBool)
                {
                    project.HasEndDate = "Yes";
                }
                else
                {
                    project.HasEndDate = "No";
                }
            }

            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(List<ExportProjectDto>), new XmlRootAttribute("Projects"));
            serializer.Serialize(new StringWriter(sb), projects, namespaces);
            return sb.ToString().Trim();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {

            var employees = context.Employees.ToList()
                .Where(x => x.EmployeesTasks.Any(x => x.Task.OpenDate >= date))
                .Select(x => new
                {
                    Username = x.Username,
                    Tasks = x.EmployeesTasks
                    .Where(x => x.Task.OpenDate >= date)
                    .OrderByDescending(x => x.Task.DueDate)
                    .ThenBy(x => x.Task.Name)
                    .Select(x => new
                    {
                        TaskName = x.Task.Name,
                        OpenDate = x.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                        DueDate = x.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                        LabelType = x.Task.LabelType.ToString(),
                        ExecutionType = x.Task.ExecutionType.ToString(),
                    })
                    .ToList()
                })
                .OrderByDescending(x => x.Tasks.Count)
                .ThenBy(x => x.Username)
                .Take(10)
                .ToList();

            var suppliersJson = JsonConvert.SerializeObject(employees, Formatting.Indented);
            return suppliersJson;
        }
    }
}