using System;
using System.Collections.Generic;
using System.Linq;
using TeachingAppAPI.Helpers;
using TeachingAppAPI.Models;
using TeachingAppAPI.Data;

namespace TeachingAppAPI.Services
{
    public class QuestionService : IQuestionService
    {
        private TestDB_Phase2Context _context;

        public QuestionService(TestDB_Phase2Context context)
        {
            _context = context;
        }


        public Question CreateQuestion(Question question)
        {
            if (question.QuizId == null)
            {
                throw new CustomException("Question not associated with a Quiz.");
            }
            else if (string.IsNullOrWhiteSpace(question.QuestionText))
            {
                throw new CustomException("Question is required");
            }
            else if (DoesQuestionAlreadyExist(question.QuestionText))
            {
                throw new CustomException("Question already exists");
            }
            else
            {
                _context.Question.Add(question); ;
                _context.SaveChanges();
                Console.WriteLine("created quiz");
                return question;
            }
        }


        private Boolean DoesQuestionAlreadyExist(string questionText)
        {
            if (_context.Question.Any(x => x.QuestionText == questionText))
            {
                return true;
            }
            return false;
        }

    }
}
