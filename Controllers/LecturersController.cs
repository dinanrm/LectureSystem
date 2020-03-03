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
    public class LecturersController : ControllerBase
    {
        private readonly LectureSystemDbContext _context;

        public LecturersController(LectureSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/Lecturers
        /// <summary>
        /// Get all lecturers
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/lecturers
        ///
        /// </remarks>
        /// <response code="200">Returns all of lecturer entity.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lecturers>>> GetLecturers()
        {
            var lecturers = await _context.Lecturers
                .Select(s => new {
                    s.LecturerId,
                    s.Name,
                    s.Email,
                    s.LastLogin,
                })
                .ToListAsync();

            return Ok(lecturers);
        }

        // GET: api/Lecturers/5
        /// <summary>
        /// Get a lecturer by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/lecturers/1
        ///
        /// </remarks>
        /// <param name="id">A lecturer id</param>
        /// <response code="200">Returns a lecturer entity.</response>
        /// <response code="404">If the id of lecturer entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Lecturers>> GetLecturers(int id)
        {
            var lecturer = await _context.Lecturers
                .Where(x => x.LecturerId == id)
                .Select(s => new
                {
                    s.LecturerId,
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

            if (lecturer == null)
            {
                return NotFound();
            }

            return Ok(lecturer);
        }

        // PUT: api/Lecturers/5
        /// <summary>
        /// Update a lecturer by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/lecturers/1
        ///     {
        ///         "lecturerId": 1,
        ///         "name": "Ade Chandra",
        ///         "birthdate": "1973-03-03",
        ///         "phoneNumber": "0987654321",
        ///         "address": "Bandung",
        ///         "email": "adechandra@gmail.com",
        ///         "password": "adePass"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">A lecturer id</param>
        /// <param name="lecturers">A lecturer entity</param>
        /// <response code="204">Returns updated lecturer entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="404">If the id of lecturer entity is not exist</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLecturers(int id, Lecturers lecturers)
        {
            if (id != lecturers.LecturerId)
            {
                return BadRequest();
            }

            var result = await _context.Lecturers.FindAsync(id);

            result.Name = lecturers.Name;
            result.Birthdate = lecturers.Birthdate;
            result.PhoneNumber = lecturers.PhoneNumber;
            result.Address = lecturers.Address;
            result.Email = lecturers.Email;
            result.Password = EncryptPassword(lecturers.Password);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LecturersExists(id))
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

        // POST: api/Lecturers
        /// <summary>
        /// Add a new lecturer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/lecturers
        ///     {
        ///         "name": "Ade Chandra",
        ///         "birthdate": "1973-03-03",
        ///         "phoneNumber": "0987654321",
        ///         "address": "Bandung",
        ///         "email": "adechandra@gmail.com",
        ///         "password": "adePass"
        ///     }
        ///
        /// </remarks>
        /// <param name="lecturers">A lecturer entity</param>
        /// <response code="201">Returns the created lecturer entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult<Lecturers>> PostLecturers(Lecturers lecturers)
        {
            var result = await _context.Lecturers
                .Where(l => l.Email == lecturers.Email)
                .FirstOrDefaultAsync();

            if (result != null)
            {
                return Conflict("Email has been used");
            }

            lecturers.Password = EncryptPassword(lecturers.Password);

            _context.Lecturers.Add(lecturers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLecturers", new { id = lecturers.LecturerId }, lecturers);
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
        [HttpPost("Login")]
        public async Task<ActionResult<Lecturers>> Login(Lecturers lecturers)
        {
            var result = await _context.Lecturers.FirstOrDefaultAsync(l => l.Email == lecturers.Email);

            if (result == null)
            {
                return Unauthorized("The email or password is incorrect");
            }

            var validationStatus = VerifyPassword(lecturers.Password, result.Password);

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

        // GET: api/lecturers/Logout
        /// <summary>
        /// Logout as a lecturer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/lecturers/logout
        ///
        /// </remarks>
        /// <param name="returnUrl">A url to redirect to</param>
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout([FromQuery] string returnUrl)
        {
            await HttpContext.SignOutAsync();

            return Redirect(returnUrl);
        }

        // DELETE: api/Lecturers/5
        /// <summary>
        /// Delete a lecturer by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/lecturers/1
        ///
        /// </remarks>
        /// <param name="id">A lecturer id</param>
        /// <response code="200">Returns deleted lecturer entity.</response>
        /// <response code="404">If the id of lecturer entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lecturers>> DeleteLecturers(int id)
        {
            var lecturers = await _context.Lecturers.FindAsync(id);
            if (lecturers == null)
            {
                return NotFound();
            }

            _context.Lecturers.Remove(lecturers);
            await _context.SaveChangesAsync();

            return lecturers;
        }

        private bool LecturersExists(int id)
        {
            return _context.Lecturers.Any(e => e.LecturerId == id);
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
