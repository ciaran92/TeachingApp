using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachingAppAPI.Data;
using TeachingAppAPI.Models;

namespace TeachingAppAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/UsersDashboard")]
    public class UsersDashboardController : Controller
    {

        private TestDB_Phase2Context _context;

        public UsersDashboardController(TestDB_Phase2Context context)
        {
            _context = context;
        }

        [Authorize(Policy = "RegisteredUser")]
        [HttpGet("get-teacher-dashboard")]
        public IActionResult GetCourses()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var param = new SqlParameter("@CreatorAppUserId", Convert.ToInt32(userIdClaim));

            var courses = _context.Course.FromSql($"select * from Course where CreatorAppUserId = @CreatorAppUserId and CourseStatusId = 1", param).ToList();
            var courses2 = _context.Course.FromSql($"select * from Course where CreatorAppUserId = @CreatorAppUserId and CourseStatusId = 2", param).ToList();

            var courseListToReturn = Mapper.Map<IEnumerable<CoursesListDto>>(courses);
            var courseListToReturn2 = Mapper.Map<IEnumerable<CoursesListDto>>(courses2);
            return Ok(new
            {
                status1 = courseListToReturn,
                status2 = courseListToReturn2
            });
        }
    }
}