using Quiz.Data;
using Quiz.Models.DatabaseModels;
using Quiz.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext dbContext;

        public QuestionService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Add(string title, string quizId)
        {
            var question = new Question
            {
                Title = title,
                QuizId = quizId
            };

            dbContext.Questions.Add(question);
            dbContext.SaveChanges();

            return question.Id;
        }
    }
}
