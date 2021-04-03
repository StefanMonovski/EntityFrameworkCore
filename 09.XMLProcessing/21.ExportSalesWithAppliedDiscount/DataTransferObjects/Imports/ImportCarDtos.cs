using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.Imports
{
    [XmlType("Car")]
    public class ImportCarDto
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public long TraveledDistance { get; set; }

        [XmlArray("parts")]
        public List<ImportPartIdDto> PartsId { get; set; }
    }

    [XmlType("partId")]
    public class ImportPartIdDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
