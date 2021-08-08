using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.ViewModels
{
    public class ResultQuestionViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string UserAnswerId { get; set; }

        public IEnumerable<ResultAnswerViewModel> ResultAnswers { get; set; }
    }
}
