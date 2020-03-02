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
    public class TeachesController : ControllerBase
    {
        private readonly LectureSystemDbContext _context;

        public TeachesController(LectureSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/Teaches
        /// <summary>
        /// Get all teaches
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/teaches
        ///
        /// </remarks>
        /// <response code="200">Returns all of teach entity.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teaches>>> GetTeaches()
        {
            return await _context.Teaches.ToListAsync();
        }

        // GET: api/Teaches/5
        /// <summary>
        /// Get a teach by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/teaches/1
        ///
        /// </remarks>
        /// <param name="id">A teach id</param>
        /// <response code="200">Returns a teach entity.</response>
        /// <response code="404">If the id of teach entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Teaches>> GetTeaches(int id)
        {
            var teaches = await _context.Teaches.FindAsync(id);

            if (teaches == null)
            {
                return NotFound();
            }

            return teaches;
        }

        // PUT: api/Teaches/5
        /// <summary>
        /// Update a teach by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/teaches/1
        ///
        /// </remarks>
        /// <param name="id">A teach id</param>
        /// <param name="teaches">A teach entity</param>
        /// <response code="204">Returns updated teach entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="404">If the id of teach entity is not exist</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeaches(int id, Teaches teaches)
        {
            if (id != teaches.TeachId)
            {
                return BadRequest();
            }

            _context.Entry(teaches).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeachesExists(id))
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

        // POST: api/Teaches
        /// <summary>
        /// Add a new teach
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/teaches
        ///
        /// </remarks>
        /// <param name="teaches">A teach entity</param>
        /// <response code="201">Returns the created teach entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult<Teaches>> PostTeaches(Teaches teaches)
        {
            _context.Teaches.Add(teaches);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeaches", new { id = teaches.TeachId }, teaches);
        }

        // DELETE: api/Teaches/5
        /// <summary>
        /// Delete a teach by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/teaches/1
        ///
        /// </remarks>
        /// <param name="id">A teach id</param>
        /// <response code="200">Returns deleted teach entity.</response>
        /// <response code="404">If the id of teach entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Teaches>> DeleteTeaches(int id)
        {
            var teaches = await _context.Teaches.FindAsync(id);
            if (teaches == null)
            {
                return NotFound();
            }

            _context.Teaches.Remove(teaches);
            await _context.SaveChangesAsync();

            return teaches;
        }

        private bool TeachesExists(int id)
        {
            return _context.Teaches.Any(e => e.TeachId == id);
        }
    }
}
