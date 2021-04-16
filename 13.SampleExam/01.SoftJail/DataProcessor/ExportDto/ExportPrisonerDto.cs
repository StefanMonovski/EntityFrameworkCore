using System;
using System.Collections.Generic;
using System.Text;

namespace SoftJail.DataProcessor.ExportDto
{
    public class ExportPrisonerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CellNumber { get; set; }

        public List<ExportOfficerDto> Officers { get; set; }

        public double TotalOfficerSalary { get; set; }
    }
}
