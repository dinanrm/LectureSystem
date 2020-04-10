using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LectureSystem.Data;
using LectureSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace LectureSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly LectureSystemDbContext _context;

        public CoursesController(LectureSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        /// <summary>
        /// Get all courses
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/courses
        ///
        /// </remarks>
        /// <response code="200">Returns all of course entity.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Courses>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        // GET: api/Courses/5
        /// <summary>
        /// Get a course by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/courses/1
        ///
        /// </remarks>
        /// <param name="id">A course id</param>
        /// <response code="200">Returns a course entity.</response>
        /// <response code="404">If the id of course entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Courses>> GetCourses(int id)
        {
            var courses = await _context.Courses.FindAsync(id);

            if (courses == null)
            {
                return NotFound();
            }

            return courses;
        }

        // PUT: api/Courses/5
        /// <summary>
        /// Update a course by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/courses/1
        ///     {
        ///         "courseId" : "1",
        ///         "name": "Manajemen Proyek",
        ///         "description": "Mata kuliah yang mempelajari tentang bagaimana melakukan manajemen proyek yang baik dan benar",
        ///         "semesterCreditUnit": 2,
        ///         "curriculum": "2015"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">A course id</param>
        /// <param name="courses">A course entity</param>
        /// <response code="204">Returns updated course entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="404">If the id of course entity is not exist</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourses(int id, Courses courses)
        {
            if (id != courses.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(courses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoursesExists(id))
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

        // POST: api/Courses
        /// <summary>
        /// Add a new course
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/courses
        ///     { 
        ///         "name": "Manajemen Proyek",
        ///         "description": "Mata kuliah yang mempelajari tentang bagaimana melakukan manajemen proyek yang baik dan benar",
        ///         "semesterCreditUnit": 2,
        ///         "curriculum": "2015"
        ///     }
        ///
        /// </remarks>
        /// <param name="courses">A course entity</param>
        /// <response code="201">Returns the created course entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult<Courses>> PostCourses(Courses courses)
        {
            _context.Courses.Add(courses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourses", new { id = courses.CourseId }, courses);
        }

        // DELETE: api/Courses/5
        /// <summary>
        /// Delete a course by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/courses/1
        ///
        /// </remarks>
        /// <param name="id">A course id</param>
        /// <response code="200">Returns deleted course entity.</response>
        /// <response code="404">If the id of course entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Courses>> DeleteCourses(int id)
        {
            var courses = await _context.Courses.FindAsync(id);
            if (courses == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(courses);
            await _context.SaveChangesAsync();

            return courses;
        }

        private bool CoursesExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}
