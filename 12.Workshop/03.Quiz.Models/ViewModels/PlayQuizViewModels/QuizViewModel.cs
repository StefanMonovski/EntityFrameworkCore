using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.ViewModels
{
    public class QuizViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}
