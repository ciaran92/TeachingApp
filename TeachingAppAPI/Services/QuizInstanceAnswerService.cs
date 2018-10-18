using System;
using System.Collections.Generic;
using System.Linq;
using TeachingAppAPI.Helpers;
using TeachingAppAPI.Models;
using TeachingAppAPI.Data;

namespace TeachingAppAPI.Services
{
    public class QuizInstanceAnswerService : IQuizInstanceAnswerService
    {
        private TestDB_Phase2Context _context;

        public QuizInstanceAnswerService(TestDB_Phase2Context context)
        {
            _context = context;
        }


        public QuizInstanceAnswer CreateQuizInstanceAnswer(QuizInstanceAnswer quizInstanceAnswer)
        {
            if (quizInstanceAnswer.QuizInstanceId == null)
            {
                throw new CustomException("quizInstanceAnswer not associated with a quizInstance.");
            }
            else if (quizInstanceAnswer.QuestionId == null)
            {
                throw new CustomException("quizInstanceAnswer not associated with a question.");
            }
            else if (quizInstanceAnswer.AnswerId == null)
            {
                throw new CustomException("quizInstanceAnswer not associated with an answer.");
            }
            else if (quizInstanceAnswer.AppUserAnswerDateTime == null)
            {
                throw new CustomException("AppUserAnswerDateTime not associated with a quizInstanceis required.");
            }
            //else if (string.IsNullOrWhiteSpace(quizInstanceAnswer.AppUserAnswer))
            //{
            //    throw new CustomException("Answer is required");
            //}
            else
            {
                _context.QuizInstanceAnswer.Add(quizInstanceAnswer); ;
                _context.SaveChanges();
                Console.WriteLine("created quizInstanceAnswer");
                return quizInstanceAnswer;
            }

        }


    }
}
