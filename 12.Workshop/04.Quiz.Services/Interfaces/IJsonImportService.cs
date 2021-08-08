using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Services.Interfaces
{
    public interface IJsonImportService
    {
        void Import(string filePath, string quizTitle);
    }
}
