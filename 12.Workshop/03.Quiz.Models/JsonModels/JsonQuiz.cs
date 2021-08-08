using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.JsonModels
{
    public class JsonQuiz
    {
        public string QuizTitle { get; set; }

        public IEnumerable<JsonQuestion> Questions { get; set; }
    }
}
