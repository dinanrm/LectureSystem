using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LectureSystem.Data;
using LectureSystem.Models;
using Microsoft.AspNetCore.Authentication;

namespace LectureSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly LectureSystemDbContext _context;

        public StudentsController(LectureSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        /// <summary>
        /// Get all students
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/students
        ///
        /// </remarks>
        /// <response code="200">Returns all of student entity.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Students>>> GetStudents()
        {
            var students = await _context.Students
                .Select(s => new
                {
                    s.StudentId,
                    s.Name,
                    s.Email,
                    s.LastLogin
                })
                .ToListAsync();

            return Ok(students);
        }

        // GET: api/Students/5
        /// <summary>
        /// Get a student by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/students/1
        ///
        /// </remarks>
        /// <param name="id">A student id</param>
        /// <response code="200">Returns a student entity.</response>
        /// <response code="404">If the id of student entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Students>> GetStudents(int id)
        {
            var student = await _context.Students
                .Where(x => x.StudentId == id)
                .Select(s => new
                {
                    s.StudentId,
                    s.Name,
                    s.Birthdate,
                    s.PhoneNumber,
                    s.Address,
                    s.Email,
                    s.LastLogin,
                    s.Status,
                    s.CreatedDate,
                    s.UpdatedDate
                })
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/Students/5
        /// <summary>
        /// Update a student by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/students/1
        ///     {
        ///         "studentId": "6",
        ///         "name": "Chris Martin",
        ///         "birthdate": "1977-03-02",
        ///         "phoneNumber": "0987654321",
        ///         "address": "United Kingdom",
        ///         "email": "coldplay@gmail.com",
        ///         "password": "coldplayPass"
        ///     }
        ///     
        ///
        /// </remarks>
        /// <param name="id">A student id</param>
        /// <param name="students">A student entity</param>
        /// <response code="204">Returns updated student entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="404">If the id of student entity is not exist</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudents(int id, Students students)
        {
            if (id != students.StudentId)
            {
                return BadRequest();
            }

            var result = await _context.Students.FindAsync(id);

            result.Name = students.Name;
            result.Birthdate = students.Birthdate;
            result.PhoneNumber = students.PhoneNumber;
            result.Address = students.Address;
            result.Email = students.Email;
            result.Password = EncryptPassword(students.Password);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        /// <summary>
        /// Add a new student
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/students
        ///     {
        ///         "name": "Chris Martin",
        ///         "birthdate": "1977-03-02",
        ///         "phoneNumber": "0987654321",
        ///         "address": "United Kingdom",
        ///         "email": "coldplay@gmail.com",
        ///         "password": "coldplayPass"
        ///     }
        ///
        /// </remarks>
        /// <param name="students">A student entity</param>
        /// <response code="201">Returns the created student entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult<Students>> PostStudents(Students students)
        {
            var result = await _context.Students
                .Where(u => u.Email == students.Email)
                .FirstOrDefaultAsync();

            if (result != null)
            {
                return Conflict("Email has been used");
            }

            students.Password = EncryptPassword(students.Password);

            _context.Students.Add(students);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudents", new { id = students.StudentId }, students);
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
        /// <response code="201">Returns the created student entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="401">Email or Password is incorrect</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpPost("Login")]
        public async Task<ActionResult<Students>> Login(Students students)
        {
            var result = await _context.Students.FirstOrDefaultAsync(u => u.Email == students.Email);

            if (result == null)
            {
                return Unauthorized("The email or password is incorrect");
            }

            var validationStatus = VerifyPassword(students.Password, result.Password);

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
                result.UpdatedDate
            };

            return Ok(filtered);
        }

        // GET: api/Students/Logout
        /// <summary>
        /// Logout as a student
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/students/logout
        ///
        /// </remarks>
        /// <param name="returnUrl">A url to redirect to</param>
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout([FromQuery] string returnUrl)
        {
            await HttpContext.SignOutAsync();

            return Redirect(returnUrl);
        }

        // DELETE: api/Students/5
        /// <summary>
        /// Delete a student by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/students/1
        ///
        /// </remarks>
        /// <param name="id">A student id</param>
        /// <response code="200">Returns deleted student entity.</response>
        /// <response code="404">If the id of student entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Students>> DeleteStudents(int id)
        {
            var students = await _context.Students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }

            _context.Students.Remove(students);
            await _context.SaveChangesAsync();

            return students;
        }

        private bool StudentsExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }

        protected string EncryptPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        protected bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
        }
    }
}
