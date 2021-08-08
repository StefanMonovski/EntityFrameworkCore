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
    public class QuizService : IQuizService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUserService userService;

        public QuizService(ApplicationDbContext dbContext, IUserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }

        public string Add(string title)
        {
            var quiz = new Models.DatabaseModels.Quiz
            {
                Title = title
            };

            dbContext.Quizes.Add(quiz);
            dbContext.SaveChanges();

            return quiz.Id;
        }

        public int GetQuizCount()
        {
            var quizCount = dbContext.Quizes
                .Count();

            return quizCount;
        }

        public string GetQuizTitle(string quizId)
        {
            var quizTitle = dbContext.Quizes
                .Where(x => x.Id == quizId)
                .Select(x => x.Title)
                .FirstOrDefault();

            return quizTitle;
        }

        public int GetMaxPoints(string quizId)
        {
            var maxPoints = dbContext.Questions
                .Where(x => x.QuizId == quizId)
                .Count();

            return maxPoints;
        }

        public QuizViewModel GetQuizById(string quizId)
        {
            var viewModel = dbContext.Quizes
                .Select(x => new QuizViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Questions = x.Questions.Select(x => new QuestionViewModel()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Answers = x.Answers.Select(x => new AnswerViewModel()
                        {
                            Id = x.Id,
                            Title = x.Title
                        })
                        .ToList()
                    })
                    .ToList()
                })
                .FirstOrDefault(x => x.Id == quizId);

            return viewModel;
        }

        public List<AllQuizesViewModel> GetAll(string identityUserId)
        {
            var viewModel = dbContext.Quizes
                .Select(x => new AllQuizesViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    QuestionsCount = x.Questions.Count,
                    HasUserPlayed = userService.HasUserPlayed(identityUserId, x.Id)
                })
                .OrderByDescending(x => x.QuestionsCount)
                .ToList();

            return viewModel;
        }
    }
}
