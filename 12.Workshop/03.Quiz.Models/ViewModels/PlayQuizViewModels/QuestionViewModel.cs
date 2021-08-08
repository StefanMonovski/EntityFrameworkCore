using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.ViewModels
{
    public class QuestionViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<AnswerViewModel> Answers { get; set; }
    }
}
