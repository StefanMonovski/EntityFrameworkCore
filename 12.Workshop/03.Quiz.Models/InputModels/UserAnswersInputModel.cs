using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.InputModels
{
    public class UserAnswersInputModel
    {
        public List<string> Questions { get; set; }

        public List<string> Answers { get; set; }
    }
}
