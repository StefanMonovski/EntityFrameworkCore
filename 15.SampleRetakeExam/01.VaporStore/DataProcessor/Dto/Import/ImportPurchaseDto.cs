using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Import
{
    [XmlType("Purchase")]
    public class ImportPurchaseDto
    {
        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlElement]
        public string Type { get; set; }

        [XmlElement]
        public string Key { get; set; }

        [XmlElement]
        public string Card { get; set; }

        [XmlElement]
        public string Date { get; set; }
    }
}
