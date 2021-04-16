using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportPrisonerDto
    {
        public string FullName { get; set; }

        public string NickName { get; set; }

        public int Age { get; set; }

        public DateTime IncarcerationDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public decimal? Bail { get; set; }

        public int? CellId { get; set; }

        public List<ImportMailDto> Mails { get; set; }
    }
}
