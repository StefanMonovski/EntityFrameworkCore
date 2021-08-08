using Quiz.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Services.Interfaces
{
    public interface IUserService
    {
        int GetUserCount();

        bool HasUserPlayed(string identityUserId, string quizId);

        UserStatisticsViewModel GetUserStats(string identityUserId);

        AllRankingsViewModel GetAllUserRankings();
    }
}
