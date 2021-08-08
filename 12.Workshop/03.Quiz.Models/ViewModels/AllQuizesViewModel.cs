using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.ViewModels
{
    public class AllQuizesViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int QuestionsCount { get; set; }

        public bool HasUserPlayed { get; set; }
    }
}
