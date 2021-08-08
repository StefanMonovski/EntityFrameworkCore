using Quiz.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Services.Interfaces
{
    public interface IUserAnswerService
    {
        string Add(string identityUserId, string questionId, string answerId);

        int GetPoints(string identityUserId, string quizId);

        SubmitAnswersViewModel GetSubmitAnswersResultsById(string identityUserId, string quizId);

        ResultQuizViewModel GetUserResultQuizById(string identityUserId, string quizId);

        //get all user results
    }
}
