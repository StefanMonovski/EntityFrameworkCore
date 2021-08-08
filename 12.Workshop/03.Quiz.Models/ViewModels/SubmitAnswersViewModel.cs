using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.ViewModels
{
    public class SubmitAnswersViewModel
    {
        public string QuizTitle { get; set; }

        public int Points { get; set; }

        public int MaxPoints { get; set; }
    }
}
