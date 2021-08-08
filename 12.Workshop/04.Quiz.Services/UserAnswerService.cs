using Quiz.Data;
using Quiz.Models.DatabaseModels;
using Quiz.Models.ViewModels;
using Quiz.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Services
{
    public class UserAnswerService : IUserAnswerService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IQuizService quizService;

        public UserAnswerService(ApplicationDbContext dbContext, IQuizService quizService)
        {
            this.dbContext = dbContext;
            this.quizService = quizService;
        }

        public string Add(string identityUserId, string questionId, string answerId)
        {
            var userAnswer = new UserAnswer
            {
                IdentityUserId = identityUserId,
                QuestionId = questionId,
                AnswerId = answerId
            };

            dbContext.UserAnswers.Add(userAnswer);
            dbContext.SaveChanges();

            return userAnswer.Id;
        }


        public int GetPoints(string identityUserId, string quizId)
        {
            var points = dbContext.UserAnswers
                .Where(x => x.IdentityUserId == identityUserId && x.Question.QuizId == quizId)
                .Sum(x => x.Answer.Points);

            return points;
        }

        public SubmitAnswersViewModel GetSubmitAnswersResultsById(string identityUserId, string quizId)
        {
            var viewModel = new SubmitAnswersViewModel
            {
                QuizTitle = quizService.GetQuizTitle(quizId),
                Points = GetPoints(identityUserId, quizId),
                MaxPoints = quizService.GetMaxPoints(quizId)
            };

            return viewModel;
        }

        public ResultQuizViewModel GetUserResultQuizById(string identityUserId, string quizId)
        {
            var viewModel = dbContext.Quizes
                .Select(x => new ResultQuizViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Points = GetPoints(identityUserId, quizId),
                    MaxPoints = quizService.GetMaxPoints(quizId),
                    ResultQuestions = x.Questions.Select(x => new ResultQuestionViewModel()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        UserAnswerId = x.UsersAnswers
                            .Where(x => x.IdentityUserId == identityUserId)
                            .Select(x => x.AnswerId)
                            .FirstOrDefault(),
                        ResultAnswers = x.Answers.Select(x => new ResultAnswerViewModel()
                        {
                            Id = x.Id,
                            Title = x.Title,
                            IsCorrect = x.Points != 0
                        })
                        .ToList()
                    })
                    .ToList()
                })
                .FirstOrDefault(x => x.Id == quizId);

            return viewModel;
        }
    }
}
