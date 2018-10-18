using System;
using System.Collections.Generic;
using System.Linq;
using TeachingAppAPI.Helpers;
using TeachingAppAPI.Models;
using TeachingAppAPI.Data;

namespace TeachingAppAPI.Services
{
    public class QuizInstanceService : IQuizInstanceService
    {
        private TestDB_Phase2Context _context;

        public QuizInstanceService(TestDB_Phase2Context context)
        {
            _context = context;
        }


        public QuizInstance CreateQuizInstance(QuizInstance quizInstance)
        {
            if (quizInstance.EnrolmentId == null)
            {
                throw new CustomException("quizInstance not associated with a course.");
            }
            else if (quizInstance.QuizId == null)
            {
                throw new CustomException("quizInstance not associated with a quiz.");
            }
            else
            {
                _context.QuizInstance.Add(quizInstance);
                _context.SaveChanges();
                Console.WriteLine("created quizInstance");
                return quizInstance;
            }

        }
    }
}
