using Microsoft.AspNetCore.Mvc;
using Quiz.Models.InputModels;
using Quiz.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionService questionService;
        private readonly IAnswerService answerService;

        public QuestionController(IQuestionService questionService, IAnswerService answerService)
        {
            this.questionService = questionService;
            this.answerService = answerService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(QuestionAnswersInputModel inputModel)
        {
            string quizId = Request.Query["quizId"];
            string title = inputModel.Title;
            string questionId = questionService.Add(title, quizId);

            int trueAnswerCount = int.Parse(inputModel.TrueAnswer);
            for (int i = 0; i < 4; i++)
            {
                if (trueAnswerCount == i)
                {
                    answerService.Add(inputModel.Answers[i], 1, questionId);
                }
                else
                {
                    answerService.Add(inputModel.Answers[i], 0, questionId);
                }
            }

            switch (inputModel.Button)
            {
                case "add": return View();
                case "complete": return RedirectToAction("All", "Quiz");
                default: return RedirectToAction("Error", "Home");
            }
        }
    }
}
 