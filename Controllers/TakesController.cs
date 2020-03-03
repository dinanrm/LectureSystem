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
    public class TakesController : ControllerBase
    {
        private readonly LectureSystemDbContext _context;

        public TakesController(LectureSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/Takes
        /// <summary>
        /// Get all takes
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/takes
        ///
        /// </remarks>
        /// <response code="200">Returns all of take entity.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Takes>>> GetTakes()
        {
            return await _context.Takes.ToListAsync();
        }

        // GET: api/Takes/5
        /// <summary>
        /// Get a take by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/takes/1
        ///
        /// </remarks>
        /// <param name="id">A take id</param>
        /// <response code="200">Returns a take entity.</response>
        /// <response code="404">If the id of take entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Takes>> GetTakes(int id)
        {
            var takes = await _context.Takes
                .Where(t => t.TakeId == id)
                .Include(x => x.Course)
                .Include(x => x.Semester)
                .Include(x => x.Student)
                .Include(x => x.CourseScores)
                .FirstOrDefaultAsync();

            if (takes == null)
            {
                return NotFound();
            }

            return takes;
        }

        // PUT: api/Takes/5
        /// <summary>
        /// Update a take by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/takes/1
        ///     {
        ///         "takeId: 1,
        ///         "studentId": 1,
        ///         "semesterId": 1,
        ///         "courseId": 1,
        ///         "academicYear": "2015-2016"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">A take id</param>
        /// <param name="takes">A take entity</param>
        /// <response code="204">Returns updated take entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="404">If the id of take entity is not exist</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTakes(int id, Takes takes)
        {
            if (id != takes.TakeId)
            {
                return BadRequest();
            }

            _context.Entry(takes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TakesExists(id))
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

        // POST: api/Takes
        /// <summary>
        /// Add a new take
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/takes
        ///     {
        ///         "studentId": 1,
        ///         "semesterId": 1,
        ///         "courseId": 1,
        ///         "academicYear": "2015-2016"
        ///     }
        ///
        /// </remarks>
        /// <param name="takes">A take entity</param>
        /// <response code="201">Returns the created take entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult<Takes>> PostTakes(Takes takes)
        {
            _context.Takes.Add(takes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTakes", new { id = takes.TakeId }, takes);
        }

        // DELETE: api/Takes/5
        /// <summary>
        /// Delete a take by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/takes/1
        ///
        /// </remarks>
        /// <param name="id">A take id</param>
        /// <response code="200">Returns deleted take entity.</response>
        /// <response code="404">If the id of take entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Takes>> DeleteTakes(int id)
        {
            var takes = await _context.Takes
                .Where(t => t.TakeId == id)
                .Include(x => x.Course)
                .Include(x => x.Semester)
                .Include(x => x.Student)
                .Include(x => x.CourseScores)
                .FirstOrDefaultAsync();

            if (takes == null)
            {
                return NotFound();
            }

            _context.Takes.Remove(takes);
            await _context.SaveChangesAsync();

            return takes;
        }

        private bool TakesExists(int id)
        {
            return _context.Takes.Any(e => e.TakeId == id);
        }
    }
}
