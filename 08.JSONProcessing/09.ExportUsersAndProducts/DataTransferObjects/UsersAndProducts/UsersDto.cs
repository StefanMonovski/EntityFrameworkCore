using System;
using System.Collections.Generic;
using System.Text;

namespace ProductShop.DataTransferObjects.UsersAndProducts
{
    public class UsersDto
    {
        public int UsersCount { get; set; }

        public List<UserDto> Users { get; set; }
    }
}
