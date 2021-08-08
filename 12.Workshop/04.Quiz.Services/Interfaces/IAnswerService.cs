using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Services.Interfaces
{
    public interface IAnswerService
    {
        string Add(string title, int points, string questionId);
    }
}
