using System;
using System.Collections.Generic;
using System.Text;

namespace ProductShop.DataTransferObjects.UsersAndProducts
{
    public class SoldProductsDto
    {
        public int? Count { get; set; }

        public List<ProductDto> Products { get; set; }
    }
}
