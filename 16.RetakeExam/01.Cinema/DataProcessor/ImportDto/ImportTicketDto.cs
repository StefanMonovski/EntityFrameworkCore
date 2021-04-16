using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Ticket")]
    public class ImportTicketDto
    {
        [XmlElement]
        public int ProjectionId { get; set; }

        [XmlElement]
        public decimal Price { get; set; }
    }
}
