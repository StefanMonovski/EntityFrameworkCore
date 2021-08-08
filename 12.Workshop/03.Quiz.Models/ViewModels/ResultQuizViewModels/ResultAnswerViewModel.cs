using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.ViewModels
{
    public class ResultAnswerViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public bool IsCorrect { get; set; }
    }
}
