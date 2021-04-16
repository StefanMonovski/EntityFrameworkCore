using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ExportProjectDto
    {
        [XmlAttribute]
        public int TasksCount { get; set; }

        [XmlElement]
        public string ProjectName { get; set; }

        [XmlIgnore]
        public bool HasEndDateBool { get; set; }

        [XmlElement]
        public string HasEndDate { get; set; }

        [XmlArray("Tasks")]
        [XmlArrayItem("Task")]
        public List<ExportTaskDto> Tasks { get; set; }
    }
}
