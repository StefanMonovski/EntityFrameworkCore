using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportCardDto
    {
        public string Number { get; set; }

        public string CVC { get; set; }

        public string Type { get; set; }
    }
}
