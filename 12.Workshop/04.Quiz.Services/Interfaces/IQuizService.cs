using Quiz.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Services.Interfaces
{
    public interface IQuizService
    {
        string Add(string title);

        int GetQuizCount();

        string GetQuizTitle(string quizId);

        int GetMaxPoints(string quizId);

        QuizViewModel GetQuizById(string quizId);

        List<AllQuizesViewModel> GetAll(string identityUserId);
    }
}
