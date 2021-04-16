namespace SoftJail.DataProcessor
{
    using AutoMapper;
    using Data;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var departmentsDto = JsonConvert.DeserializeObject<IEnumerable<ImportDepartmentDto>>(jsonString);

            StringBuilder sb = new StringBuilder();
            var departments = new List<Department>();
            foreach (var departmentDto in departmentsDto)
            {
                var department = Mapper.Map<Department>(departmentDto);

                if (!IsValid(department) || !department.Cells.All(IsValid) || !department.Cells.Any())
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                departments.Add(department);
                sb.AppendLine($"Imported {department.Name} with {department.Cells.Count} cells");
            }

            context.Departments.AddRange(departments);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var dateTimeConverter = new IsoDateTimeConverter { Culture = CultureInfo.InvariantCulture, DateTimeFormat = "dd/MM/yyyy" };
            var prisonersDto = JsonConvert.DeserializeObject<IEnumerable<ImportPrisonerDto>>(jsonString, dateTimeConverter);
            
            StringBuilder sb = new StringBuilder();
            var prisoners = new List<Prisoner>();
            foreach (var prisonerDto in prisonersDto)
            {
                var prisoner = Mapper.Map<Prisoner>(prisonerDto);

                if (!IsValid(prisoner) || !prisoner.Mails.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                prisoners.Add(prisoner);
                sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
            }

            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(List<ImportOfficerDto>), new XmlRootAttribute("Officers"));
            var officersDto = (List<ImportOfficerDto>)serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();
            var officers = new List<Officer>();
            foreach (var officerDto in officersDto)
            {
                if (!IsValid(officerDto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var officer = Mapper.Map<Officer>(officerDto);

                if (!IsValid(officer))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                foreach (var prisonerId in officerDto.Prisoners)
                {
                    var officerPrisoner = new OfficerPrisoner
                    {
                        Officer = officer,
                        PrisonerId = prisonerId.Id
                    };
                    officer.OfficerPrisoners.Add(officerPrisoner);
                }

                officers.Add(officer);
                sb.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count} prisoners)");
            }

            context.Officers.AddRange(officers);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}