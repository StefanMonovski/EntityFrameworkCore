using Microsoft.AspNetCore.Mvc;
using Quiz.Models.ViewModels;
using Quiz.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Quiz.Web.Controllers
{
    public class QuizController : Controller
    {
        private readonly IQuizService quizService;
        private readonly IUserAnswerService userAnswerService;

        public QuizController(IQuizService quizService, IUserAnswerService userAnswerService)
        {
            this.quizService = quizService;
            this.userAnswerService = userAnswerService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string title)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Shared");
            }

            string quizId = quizService.Add(title);

            return RedirectToAction("Add", "Question", new { quizId });
        }

        public IActionResult All()
        {
            string identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var quizes = quizService.GetAll(identityUserId);

            return View(quizes);
        }

        public IActionResult Result()
        {
            string quizId = Request.Query["quizId"];
            string identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ResultQuizViewModel viewModel = userAnswerService.GetUserResultQuizById(identityUserId, quizId);

            return View(viewModel);
        }
    }
}
