using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.ViewModels
{
    public class AllRankingsViewModel
    {
        public string CurrentUserId { get; set; }

        public string Filter { get; set; }
        
        public List<UserRankingViewModel> Rankings { get; set; }
    }
}
