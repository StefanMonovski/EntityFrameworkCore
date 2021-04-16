using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Customer")]
    public class ImportCustomerDto
    {
        [XmlElement]
        public string FirstName { get; set; }

        [XmlElement]
        public string LastName { get; set; }

        [XmlElement]
        public int Age { get; set; }

        [XmlElement]
        public decimal Balance { get; set; }

        [XmlArray("Tickets")]
        [XmlArrayItem("Ticket")]
        public List<ImportTicketDto> Tickets { get; set; }
    }
}
