using System;
using System.Collections.Generic;
using System.Text;

namespace ProductShop.DataTransferObjects.UsersAndProducts
{
    public class UserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; } 

        public SoldProductsDto SoldProducts { get; set; }
    }
}
