using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.DataTransferObjects.UsersAndProducts
{
    [XmlType("SoldProducts")]
    public class SoldProductsDto
    {
        [XmlElement("count")]
        public int? Count { get; set; }

        [XmlArray("products")]
        public List<ProductDto> SoldProducts { get; set; }
    }
}
