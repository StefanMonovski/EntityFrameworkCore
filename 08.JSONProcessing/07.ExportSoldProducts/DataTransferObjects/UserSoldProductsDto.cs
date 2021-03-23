using System;
using System.Collections.Generic;
using System.Text;

namespace ProductShop.DataTransferObjects
{
    public class UserSoldProductsDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<SoldProductDto> SoldProducts { get; set; }
    }
}
