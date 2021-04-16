using System;
using System.Collections.Generic;
using System.Text;
using VaporStore.Data.Models;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportGameDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Developer { get; set; }

        public string Genre { get; set; }

        public List<string> Tags { get; set; }
    }
}
