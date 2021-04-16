using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Project")]
    public class ImportProjectDto
    {
        [XmlElement]
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }

        [XmlIgnore]
        public DateTime OpenDateProject { get; set; }

        [XmlElement]
        [Required]
        public string OpenDate { get; set; }

        [XmlIgnore]
        public DateTime? DueDateProject { get; set; }

        [XmlElement]
        public string DueDate { get; set; }

        [XmlArray]
        public List<ImportTaskDto> Tasks { get; set; }
    }
}
