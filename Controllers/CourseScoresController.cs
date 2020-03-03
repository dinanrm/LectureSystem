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
    public class CourseScoresController : ControllerBase
    {
        private readonly LectureSystemDbContext _context;

        public CourseScoresController(LectureSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/CourseScores
        /// <summary>
        /// Get all courseScores
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/courseScores
        ///
        /// </remarks>
        /// <response code="200">Returns all of courseScore entity.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseScores>>> GetCourseScores()
        {
            return await _context.CourseScores.ToListAsync();
        }

        // GET: api/CourseScores/5
        /// <summary>
        /// Get a courseScore by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/courseScores/1
        ///
        /// </remarks>
        /// <param name="id">A courseScore id</param>
        /// <response code="200">Returns a courseScore entity.</response>
        /// <response code="404">If the id of courseScore entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseScores>> GetCourseScores(int id)
        {
            var courseScores = await _context.CourseScores.FindAsync(id);

            if (courseScores == null)
            {
                return NotFound();
            }

            return courseScores;
        }

        // PUT: api/CourseScores/5
        /// <summary>
        /// Update a courseScore by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/courseScores/1
        ///     {
        ///         "courseScoreId": 1,
        ///         "takeId": 1,
        ///         "scoreId": 1
        ///     }
        ///     
        ///
        /// </remarks>
        /// <param name="id">A courseScore id</param>
        /// <param name="courseScores">A courseScore entity</param>
        /// <response code="204">Returns updated courseScore entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="404">If the id of courseScore entity is not exist</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseScores(int id, CourseScores courseScores)
        {
            if (id != courseScores.CourseScoreId)
            {
                return BadRequest();
            }

            _context.Entry(courseScores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseScoresExists(id))
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

        // POST: api/CourseScores
        /// <summary>
        /// Add a new courseScore
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/courseScores
        ///     {
        ///         "takeId": 1,
        ///         "scoreId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="courseScores">A courseScore entity</param>
        /// <response code="201">Returns the created courseScore entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult<CourseScores>> PostCourseScores(CourseScores courseScores)
        {
            _context.CourseScores.Add(courseScores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourseScores", new { id = courseScores.CourseScoreId }, courseScores);
        }

        // DELETE: api/CourseScores/5
        /// <summary>
        /// Delete a courseScore by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/courseScores/1
        ///
        /// </remarks>
        /// <param name="id">A courseScore id</param>
        /// <response code="200">Returns deleted courseScore entity.</response>
        /// <response code="404">If the id of courseScore entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseScores>> DeleteCourseScores(int id)
        {
            var courseScores = await _context.CourseScores.FindAsync(id);
            if (courseScores == null)
            {
                return NotFound();
            }

            _context.CourseScores.Remove(courseScores);
            await _context.SaveChangesAsync();

            return courseScores;
        }

        private bool CourseScoresExists(int id)
        {
            return _context.CourseScores.Any(e => e.CourseScoreId == id);
        }
    }
}
