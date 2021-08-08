using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.ViewModels
{
    public class UserRankingViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public UserStatisticsViewModel Statistics { get; set; }
    }
}
