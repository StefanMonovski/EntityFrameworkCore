using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Export
{
    [XmlType("User")]
    public class ExportUserDto
    {
        [XmlAttribute("username")]
        public string Username { get; set; }

        [XmlArray("Purchases")]
        [XmlArrayItem("Purchase")]
        public List<ExportPurchaseDto> Purchases { get; set; }

        [XmlElement]
        public decimal TotalSpent { get; set; }
    }
}
