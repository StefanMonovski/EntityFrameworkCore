﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DataTransferObjects
{
    public class CustomerDto
    {
        public string Name { get; set; }

        public string BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }
    }
}
