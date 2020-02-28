using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LectureSystem.Data;
using LectureSystem.Models;

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
            return await _context.Students.ToListAsync();
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
            var students = await _context.Students.FindAsync(id);

            if (students == null)
            {
                return NotFound();
            }

            return students;
        }

        // PUT: api/Students/5
        /// <summary>
        /// Update a student by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/students/1
        ///
        /// </remarks>
        /// <param name="id">A student id</param>
        /// <param name="students">A student entity</param>
        /// <response code="204">Returns updated student entity.</response>
        /// <response code="400">If the id of student entity in the query and json request is different.</response>
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

            _context.Entry(students).State = EntityState.Modified;

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
        ///
        /// </remarks>
        /// <param name="students">A student entity</param>
        /// <response code="201">Returns the created student entity.</response>
        [ProducesResponseType(201)]
        [HttpPost]
        public async Task<ActionResult<Students>> PostStudents(Students students)
        {
            _context.Students.Add(students);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudents", new { id = students.StudentId }, students);
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
    }
}
