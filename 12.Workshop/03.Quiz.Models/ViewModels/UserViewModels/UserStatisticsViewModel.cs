using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.ViewModels
{
    public class UserStatisticsViewModel
    {
        public int CompletedQuizes { get; set; }

        public int IncompletedQuizes { get; set; }

        public int CorrectAnswers { get; set; }

        public int IncorrectAnswers { get; set; }

        public int SuccessRate { get; set; }

        public int PerfectScores { get; set; }
    }
}
 