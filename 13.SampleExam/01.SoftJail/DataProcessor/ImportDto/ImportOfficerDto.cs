using SoftJail.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
    public class ImportOfficerDto
    {
        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public decimal Money { get; set; }

        [EnumDataType(typeof(Position))]
        [XmlElement]
        public string Position { get; set; }

        [EnumDataType(typeof(Weapon))]
        [XmlElement]
        public string Weapon { get; set; }

        [XmlElement]
        public int DepartmentId { get; set; }
         
        [XmlArray]
        public List<ImportPrisonerIdDto> Prisoners { get; set; }
    }
}
