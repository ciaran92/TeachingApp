using System;
using System.Collections.Generic;
using System.Linq;
using TeachingAppAPI.Helpers;
using TeachingAppAPI.Models;
using TeachingAppAPI.Data;

namespace TeachingAppAPI.Services
{
    public class QuizService : IQuizService
    {
        private TestDB_Phase2Context _context;

        public QuizService(TestDB_Phase2Context context)
        {
            _context = context;
        }


        public Quiz CreateQuiz(Quiz quiz)
        {
            if (quiz.TopicId == null)
            {
                throw new CustomException("Quiz not associated with a topic.");
            }
            else if (string.IsNullOrWhiteSpace(quiz.QuizDesc))
            {
                throw new CustomException("Quiz description is required");
            }
            else if (DoesQuizAlreadyExist(quiz.QuizId))
            {
                throw new CustomException("Topic name already exists");
            }
            else
            {
                _context.Quiz.Add(quiz); ;
                _context.SaveChanges();
                Console.WriteLine("created quiz");
                return quiz;
            }

        }

        private Boolean DoesQuizAlreadyExist(int quizId)
        {
            if (_context.Quiz.Any(x => x.QuizId == quizId))
            {
                return true;
            }
            return false;
        }




    }
}
