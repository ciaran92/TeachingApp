using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachingAppAPI.Entities;

namespace TeachingAppAPI.Services
{
    public interface IQuizInstanceAnswerService
    {
        QuizInstanceAnswer CreateQuizInstanceAnswer(QuizInstanceAnswer quizInstanceAnswer);
    }
}
