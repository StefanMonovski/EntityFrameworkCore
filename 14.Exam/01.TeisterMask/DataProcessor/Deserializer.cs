namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.Data.Models;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ImportDto;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(List<ImportProjectDto>), new XmlRootAttribute("Projects"));
            var projectsDto = (List<ImportProjectDto>)serializer.Deserialize(new StringReader(xmlString));

            var projects = new List<Project>();
            StringBuilder sb = new StringBuilder();
            foreach (var projectDto in projectsDto)
            {
                if (!IsValid(projectDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var isValidOpenDateProject = DateTime.TryParseExact(projectDto.OpenDate, "dd/MM/yyyy", 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime openDateProject);

                if (!isValidOpenDateProject)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                else
                {
                    projectDto.OpenDateProject = openDateProject;
                }

                if (string.IsNullOrEmpty(projectDto.DueDate))
                {
                    projectDto.DueDateProject = null;
                }
                else
                {
                    var isValidDueDateProject = DateTime.TryParseExact(projectDto.DueDate, "dd/MM/yyyy", 
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dueDateProject);

                    if (!isValidDueDateProject)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    else
                    {
                        projectDto.DueDateProject = dueDateProject;
                    }
                }                

                var tasks = new List<ImportTaskDto>();
                foreach (var taskDto in projectDto.Tasks)
                {
                    var isValidOpenDateTask = DateTime.TryParseExact(taskDto.OpenDate, "dd/MM/yyyy", 
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime openDateTask);
                    var isValidDueDateTask = DateTime.TryParseExact(taskDto.DueDate, "dd/MM/yyyy", 
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dueDateTask);

                    if (!isValidOpenDateTask || !isValidDueDateTask)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    else
                    {
                        taskDto.OpenDateTask = openDateTask;
                        taskDto.DueDateTask = dueDateTask;
                    }

                    if (!IsValid(taskDto) || taskDto.OpenDateTask < projectDto.OpenDateProject)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (projectDto.DueDateProject.HasValue && taskDto.DueDateTask > projectDto.DueDateProject)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    tasks.Add(taskDto);
                }

                var project = new Project
                {
                    Name = projectDto.Name,
                    OpenDate = projectDto.OpenDateProject,
                    DueDate = projectDto.DueDateProject,
                    Tasks = tasks.Select(x => new Task
                    {
                        Name = x.Name,
                        OpenDate = x.OpenDateTask,
                        DueDate = x.DueDateTask,
                        ExecutionType = (ExecutionType)x.ExecutionType,
                        LabelType = (LabelType)x.LabelType,
                    })
                    .ToList()
                };

                projects.Add(project);
                sb.AppendLine(string.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count));
            }

            context.Projects.AddRange(projects);
            context.SaveChanges();
            return sb.ToString().Trim();
        }
        
        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var employeesDto = JsonConvert.DeserializeObject<IEnumerable<ImportEmployeeDto>>(jsonString);

            var employees = new List<Employee>();
            StringBuilder sb = new StringBuilder();
            foreach (var employeeDto in employeesDto)
            {
                var employee = Mapper.Map<Employee>(employeeDto);

                if (!IsValid(employee))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                foreach (var taskId in employeeDto.Tasks.Distinct())
                {
                    var task = context.Tasks.Find(taskId);
                    if (task == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var employeeTask = new EmployeeTask
                    {
                        Employee = employee,
                        Task = task
                    };
                    employee.EmployeesTasks.Add(employeeTask);
                }

                employees.Add(employee);
                sb.AppendLine(String.Format(SuccessfullyImportedEmployee, employee.Username, employee.EmployeesTasks.Count));
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}