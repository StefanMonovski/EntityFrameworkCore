using System;
using System.Collections.Generic;
using System.Text;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportDepartmentDto
    {
        public string Name { get; set; }

        public List<ImportCellDto> Cells { get; set; }
    }
}
