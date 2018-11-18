using System;
using System;
using System.Collections.Generic;
using System.Linq;
using TeachingAppAPI.Helpers;
using TeachingAppAPI.Entities;
using TeachingAppAPI.Data;

namespace TeachingAppAPI.Services
{
    public class AnswerService : IAnswerService
    {
        private TestDB_Phase2Context _context;

        public AnswerService(TestDB_Phase2Context context)
        {
            _context = context;
        }


        public Answer CreateAnswer(Answer answer)
        {
            if (answer.QuestionId == null)
            {
                throw new CustomException("Answer not associated with a question.");
            }
            else if (string.IsNullOrWhiteSpace(answer.AnswerText))
            {
                throw new CustomException("Answer text is required");
            }
            //else if (DoesAnswerAlreadyExist(answer.AnswerText)) // This would be expensive!! Clustered Index might make it okay, but is it necessary in the first place?
            //{
            //    throw new CustomException("Topic name already exists");
            //}
            else
            {
                _context.Answer.Add(answer); ;
                _context.SaveChanges();
                Console.WriteLine("created Answer");
                return answer;
            }

        }

        private Boolean DoesAnswerAlreadyExist(string answerText)
        {
            if (_context.Answer.Any(x => x.AnswerText == answerText))
            {
                return true;
            }
            return false;
        }

    }
}
