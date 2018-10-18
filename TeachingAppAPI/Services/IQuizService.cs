using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachingAppAPI.Models;

namespace TeachingAppAPI.Services
{
    public interface IQuizService
    {
        Quiz CreateQuiz(Quiz quiz);
    }
}
