using Quiz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Models.ViewModels;
using Quiz.Services.Interfaces;

namespace Quiz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IQuizService quizService;
        private readonly IUserService userService;

        public HomeController(ILogger<HomeController> logger, IQuizService quizService, IUserService userService)
        {
            this.logger = logger;
            this.quizService = quizService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            IndexViewModel viewModel = new()
            {
                Quizes = quizService.GetQuizCount(),
                Users = userService.GetUserCount()
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
