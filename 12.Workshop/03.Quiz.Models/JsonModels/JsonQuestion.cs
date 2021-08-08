using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.JsonModels
{
    public class JsonQuestion
    {
        public string QuestionTitle { get; set; }

        public IEnumerable<JsonAnswer> Answers { get; set; }
    }
}
