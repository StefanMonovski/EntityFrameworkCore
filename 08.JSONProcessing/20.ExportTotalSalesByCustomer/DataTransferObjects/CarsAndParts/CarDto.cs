﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DataTransferObjects.CarsAndParts
{
    public class CarDto
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }
    }
}
