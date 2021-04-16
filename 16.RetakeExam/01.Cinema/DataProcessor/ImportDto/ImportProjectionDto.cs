using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Projection")]
    public class ImportProjectionDto
    {
        [XmlElement]
        public int MovieId { get; set; }

        [XmlElement]
        public string DateTime { get; set; }
    }
}
