using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.ViewModels
{
    public class ResultQuizViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int Points { get; set; }

        public int MaxPoints { get; set; }

        public IEnumerable<ResultQuestionViewModel> ResultQuestions { get; set; }
    }
}
