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
    public class SemestersController : ControllerBase
    {
        private readonly LectureSystemDbContext _context;

        public SemestersController(LectureSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/Semesters
        /// <summary>
        /// Get all semesters
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/semesters
        ///
        /// </remarks>
        /// <response code="200">Returns all of semester entity.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Semesters>>> GetSemesters()
        {
            return await _context.Semesters.ToListAsync();
        }

        // GET: api/Semesters/5
        /// <summary>
        /// Get a semester by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/semesters/1
        ///
        /// </remarks>
        /// <param name="id">A semester id</param>
        /// <response code="200">Returns a semester entity.</response>
        /// <response code="404">If the id of semester entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Semesters>> GetSemesters(int id)
        {
            var semesters = await _context.Semesters.FindAsync(id);

            if (semesters == null)
            {
                return NotFound();
            }

            return semesters;
        }

        // PUT: api/Semesters/5
        /// <summary>
        /// Update a semester by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/semesters/1
        ///     {
        ///         "semesterId": "1",
        ///         "name": "1",
        ///         "description": "satu"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">A semester id</param>
        /// <param name="semesters">A semester entity</param>
        /// <response code="204">Returns updated semester entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="404">If the id of semester entity is not exist</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSemesters(int id, Semesters semesters)
        {
            if (id != semesters.SemesterId)
            {
                return BadRequest();
            }

            _context.Entry(semesters).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SemestersExists(id))
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

        // POST: api/Semesters
        /// <summary>
        /// Add a new semester
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/semesters
        ///     {
        ///         "name": "1",
        ///         "description": "satu"
        ///     }
        ///
        /// </remarks>
        /// <param name="semesters">A semester entity</param>
        /// <response code="201">Returns the created semester entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult<Semesters>> PostSemesters(Semesters semesters)
        {
            _context.Semesters.Add(semesters);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSemesters", new { id = semesters.SemesterId }, semesters);
        }

        // DELETE: api/Semesters/5
        /// <summary>
        /// Delete a semester by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/semesters/1
        ///
        /// </remarks>
        /// <param name="id">A semester id</param>
        /// <response code="200">Returns deleted semester entity.</response>
        /// <response code="404">If the id of semester entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Semesters>> DeleteSemesters(int id)
        {
            var semesters = await _context.Semesters.FindAsync(id);
            if (semesters == null)
            {
                return NotFound();
            }

            _context.Semesters.Remove(semesters);
            await _context.SaveChangesAsync();

            return semesters;
        }

        private bool SemestersExists(int id)
        {
            return _context.Semesters.Any(e => e.SemesterId == id);
        }
    }
}
