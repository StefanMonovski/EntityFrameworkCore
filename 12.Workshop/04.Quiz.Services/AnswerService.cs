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
    public class AnswerService : IAnswerService
    {
        private readonly ApplicationDbContext dbContext;

        public AnswerService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Add(string title, int points, string questionId)
        {
            var answer = new Answer
            {
                Title = title,
                Points = points,
                QuestionId = questionId
            };

            dbContext.Answers.Add(answer);
            dbContext.SaveChanges();

            return answer.Id;
        }
    }
}
