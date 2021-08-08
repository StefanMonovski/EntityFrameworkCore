using Microsoft.AspNetCore.Mvc;
using Quiz.Models.InputModels;
using Quiz.Models.ViewModels;
using Quiz.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Quiz.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IQuizService quizService;
        private readonly IUserAnswerService userAnswerService;
        private readonly IUserService userService;

        public UserController(IQuizService quizService, IUserAnswerService userAnswerService, IUserService userService)
        {
            this.quizService = quizService;
            this.userAnswerService = userAnswerService;
            this.userService = userService;
        }

        public IActionResult Play()
        {
            string quizId = Request.Query["quizId"];
            QuizViewModel viewModel = quizService.GetQuizById(quizId);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Play(UserAnswersInputModel inputModel)
        {
            string quizId = Request.Query["quizId"];
            string identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            for (int i = 0; i < inputModel.Questions.Count; i++)
            {
                userAnswerService.Add(identityUserId, inputModel.Questions[i], inputModel.Answers[i]);
            }

            return RedirectToAction("Submit", "User", new { quizId });
        }

        public IActionResult Submit()
        {
            string quizId = Request.Query["quizId"];
            string identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            SubmitAnswersViewModel viewModel = userAnswerService.GetSubmitAnswersResultsById(identityUserId, quizId);

            ViewData["quizId"] = quizId;
            return View(viewModel);
        }

        public IActionResult Statistics()
        {
            string identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserStatisticsViewModel viewModel = userService.GetUserStats(identityUserId);

            return View(viewModel);
        }

        public IActionResult Rankings()
        {
            AllRankingsViewModel viewModel = userService.GetAllUserRankings();
            viewModel.CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Rankings(string button)
        {
            AllRankingsViewModel viewModel = userService.GetAllUserRankings();
            viewModel.CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            switch (button)
            {
                case "correctAnswers": viewModel.Rankings = viewModel.Rankings.OrderByDescending(x => x.Statistics.CorrectAnswers).ToList();
                    viewModel.Filter = "correctAnswers"; break;
                case "incorrectAnswers": viewModel.Rankings = viewModel.Rankings.OrderBy(x => x.Statistics.IncorrectAnswers).ToList();
                    viewModel.Filter = "incorrectAnswers"; break;
                case "completedQuizes": viewModel.Rankings = viewModel.Rankings.OrderByDescending(x => x.Statistics.CompletedQuizes).ToList();
                    viewModel.Filter = "completedQuizes"; break;
                case "incompletedQuizes": viewModel.Rankings = viewModel.Rankings.OrderBy(x => x.Statistics.IncompletedQuizes).ToList();
                    viewModel.Filter = "incompletedQuizes"; break;
                case "successRate": viewModel.Rankings = viewModel.Rankings.OrderByDescending(x => x.Statistics.SuccessRate).ToList();
                    viewModel.Filter = "successRate"; break;
                case "perfectScores": viewModel.Rankings = viewModel.Rankings.OrderByDescending(x => x.Statistics.PerfectScores).ToList();
                    viewModel.Filter = "perfectScores"; break;
            }

            return View(viewModel);
        }
    }
}
