using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.InputModels
{
    public class QuestionAnswersInputModel
    {
        public string Title { get; set; }

        public List<string> Answers { get; set; }

        public string TrueAnswer { get; set; }

        public string Button { get; set; }
    }
}
