using System;
using System.Collections.Generic;
using System.Linq;
using TeachingAppAPI.Helpers;
using TeachingAppAPI.Models;
using TeachingAppAPI.Data;

namespace TeachingAppAPI.Services
{
    public class EnrolmentService : IEnrolmentService
    {
        private TestDB_Phase2Context _context;

        public EnrolmentService(TestDB_Phase2Context context)
        {
            _context = context;
        }

        public Enrolment CreateEnrolment(Enrolment enrolment)
        {
            if (_context.Enrolment.Any(x => x.EnrolmentId == enrolment.EnrolmentId))
            {
                throw new CustomException("User already enrolled in this course");
            }
            else
            {
                _context.Enrolment.Add(enrolment);
                _context.SaveChanges();
                Console.WriteLine("created enrolment");
                return enrolment;
            }

        }
      
    }
}
