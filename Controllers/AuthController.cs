using System;
using System.Threading.Tasks;
using LectureSystem.Data;
using LectureSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LectureSystem.Utilities;
using Microsoft.Extensions.Configuration;

namespace LectureSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LectureSystemDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(LectureSystemDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/lecturers/Login
        /// <summary>
        /// Login as a lecturer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/lecturers/login
        ///     {
        ///         "email": "adechandra@gmail.com",
        ///         "password": "adePass"
        ///     }
        ///
        /// </remarks>
        /// <param name="lecturers">A lecturer entity</param>
        /// <response code="201">Returns the created lecturer entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="401">Email or Password is incorrect</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpPost("Lecturers")]
        public async Task<ActionResult<Lecturers>> Lecturers(Lecturers lecturers)
        {
            var result = await _context.Lecturers.FirstOrDefaultAsync(l => l.Email == lecturers.Email);

            if (result == null)
            {
                return Unauthorized("The email or password is incorrect");
            }

            var validationStatus = AuthHelper.VerifyPassword(lecturers.Password, result.Password);

            if (!validationStatus)
            {
                return Unauthorized("The email or password is incorrect");
            }

            result.LastLogin = DateTime.Now;
            result.Status = true;
            await _context.SaveChangesAsync();

            var filtered = new
            {
                result.LecturerId,
                result.Name,
                result.Birthdate,
                result.PhoneNumber,
                result.Address,
                result.Email,
                result.LastLogin,
                result.Status,
                result.CreatedDate,
                result.UpdatedDate
            };

            return Ok(filtered);
        }

        // POST: api/Students/Login
        /// <summary>
        /// Login as a student
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/students/login
        ///     {
        ///         "Email" : "dinan@gmail.com",
        ///         "Password" : "myPass"
        ///     }
        ///
        /// </remarks>
        /// <param name="students">A student entity</param>
        /// <response code="200">Returns the created student entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="401">Email or Password is incorrect</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpPost("Students")]
        public async Task<ActionResult<Students>> Students(Students students)
        {
            var result = await _context.Students.FirstOrDefaultAsync(u => u.Email == students.Email);

            if (result == null)
            {
                return Unauthorized("The email or password is incorrect");
            }

            var validationStatus = AuthHelper.VerifyPassword(students.Password, result.Password);

            if (!validationStatus)
            {
                return Unauthorized("The email or password is incorrect");
            }

            result.LastLogin = DateTime.Now;
            result.Status = true;
            await _context.SaveChangesAsync();

            var filtered = new
            {
                result.StudentId,
                result.Name,
                result.Birthdate,
                result.PhoneNumber,
                result.Address,
                result.Email,
                result.LastLogin,
                result.Status,
                result.CreatedDate,
                result.UpdatedDate,
                token = AuthHelper.GenerateJwtToken(result.StudentId, _configuration["SecurityKey"]),
            };

            return Ok(filtered);
        }
    }
}
