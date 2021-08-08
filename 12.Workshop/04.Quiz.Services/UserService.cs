using Quiz.Data;
using Quiz.Models.ViewModels;
using Quiz.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int GetUserCount()
        {
            var userCount = dbContext.Users
                .Count();

            return userCount;
        }
        
        public bool HasUserPlayed(string identityUserId, string quizId)
        {
            var hasUserPlayed = dbContext.UserAnswers
                .Any(x => x.Question.QuizId == quizId && x.IdentityUserId == identityUserId);

            return hasUserPlayed;
        }

        public UserStatisticsViewModel GetUserStats(string identityUserId)
        {
            var completedQuizes = dbContext.UserAnswers
                .Where(x => x.IdentityUserId == identityUserId)
                .GroupBy(x => x.Question.QuizId)
                .Count();

            var totalQuizes = dbContext.Quizes
                .Count();

            var correctAnswers = dbContext.UserAnswers
                .Where(x => x.IdentityUserId == identityUserId && x.Answer.Points != 0)
                .Count();

            var incorrectAnswers = dbContext.UserAnswers
                .Where(x => x.IdentityUserId == identityUserId && x.Answer.Points == 0)
                .Count();

            var successRate = correctAnswers / (correctAnswers + incorrectAnswers * 1.0) * 100;
            if (incorrectAnswers == 0 && correctAnswers == 0)
            {
                successRate = 0;
            }

            var perfectScores = dbContext.UserAnswers
                .Where(x => x.IdentityUserId == identityUserId && x.Answer.Points != 0)
                .GroupBy(x => x.Question.QuizId)
                .Select(x => new
                {
                    QuizId = x.Key,
                    UserCorrectAnswers = x.Count(),
                    TotalCorrectAnswers = dbContext.Questions
                        .Where(y => y.QuizId == x.Key)
                        .Count()
                })
                .Where(x => x.UserCorrectAnswers == x.TotalCorrectAnswers)
                .Count();

            var viewModel = new UserStatisticsViewModel()
            {
                CompletedQuizes = completedQuizes,
                IncompletedQuizes = totalQuizes - completedQuizes,
                CorrectAnswers = correctAnswers,
                IncorrectAnswers = incorrectAnswers,
                SuccessRate = (int)successRate,
                PerfectScores = perfectScores
            };

            return viewModel;
        }

        public AllRankingsViewModel GetAllUserRankings()
        {
            var rankings = dbContext.Users
                .Select(x => new UserRankingViewModel()
                {
                    Id = x.Id,
                    Username = x.UserName,
                    Statistics = null
                })
                .ToList();

            foreach (var user in rankings)
            {
                user.Statistics = GetUserStats(user.Id);
            }

            var viewModel = new AllRankingsViewModel()
            {
                CurrentUserId = null,
                Filter = null,                
                Rankings = rankings
            };

            return viewModel;
        }
    }
}
