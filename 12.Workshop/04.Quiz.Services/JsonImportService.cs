using Newtonsoft.Json;
using Quiz.Models.JsonModels;
using Quiz.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Services
{
    public class JsonImportService : IJsonImportService
    {
        private readonly IQuizService quizService;
        private readonly IQuestionService questionService;
        private readonly IAnswerService answerService;

        public JsonImportService(IQuizService quizService, IQuestionService questionService, IAnswerService answerService)
        {
            this.quizService = quizService;
            this.questionService = questionService;
            this.answerService = answerService;
        }

        public void Import(string filePath, string quizTitle)
        {
            string jsonContent = File.ReadAllText(filePath);
            JsonQuiz jsonQuiz = JsonConvert.DeserializeObject<JsonQuiz>(jsonContent);

            string quizId = quizService.Add(jsonQuiz.QuizTitle);
            foreach (var jsonQuestion in jsonQuiz.Questions)
            {
                string questionId = questionService.Add(jsonQuestion.QuestionTitle, quizId);
                foreach (var jsonAnswer in jsonQuestion.Answers)
                {
                    answerService.Add(jsonAnswer.AnswerTitle, jsonAnswer.Points, questionId);
                }
            }
        }
    }
}
