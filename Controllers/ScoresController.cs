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
    public class ScoresController : ControllerBase
    {
        private readonly LectureSystemDbContext _context;

        public ScoresController(LectureSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/Scores
        /// <summary>
        /// Get all scores
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/scores
        ///
        /// </remarks>
        /// <response code="200">Returns all of score entity.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Scores>>> GetScores()
        {
            return await _context.Scores.ToListAsync();
        }

        // GET: api/Scores/5
        /// <summary>
        /// Get a score by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/scores/1
        ///
        /// </remarks>
        /// <param name="id">A score id</param>
        /// <response code="200">Returns a score entity.</response>
        /// <response code="404">If the id of score entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Scores>> GetScores(int id)
        {
            var scores = await _context.Scores.FindAsync(id);

            if (scores == null)
            {
                return NotFound();
            }

            return scores;
        }

        // PUT: api/Scores/5
        /// <summary>
        /// Update a score by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/scores/1
        ///     {
        ///         "scoreId": 1,
        ///         "minScore": 85,
        ///         "alphabet": "A",
        ///         "description": "4.00"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">A score id</param>
        /// <param name="scores">A score entity</param>
        /// <response code="204">Returns updated score entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="404">If the id of score entity is not exist</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScores(int id, Scores scores)
        {
            if (id != scores.ScoreId)
            {
                return BadRequest();
            }

            _context.Entry(scores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoresExists(id))
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

        // POST: api/Scores
        /// <summary>
        /// Add a new score
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/scores
        ///     {
        ///         "minScore": 85,
        ///         "alphabet": "A",
        ///         "description": "4.00"
        ///     }
        ///
        /// </remarks>
        /// <param name="scores">A score entity</param>
        /// <response code="201">Returns the created score entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult<Scores>> PostScores(Scores scores)
        {
            _context.Scores.Add(scores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScores", new { id = scores.ScoreId }, scores);
        }

        // DELETE: api/Scores/5
        /// <summary>
        /// Delete a score by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/scores/1
        ///
        /// </remarks>
        /// <param name="id">A score id</param>
        /// <response code="200">Returns deleted score entity.</response>
        /// <response code="404">If the id of score entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Scores>> DeleteScores(int id)
        {
            var scores = await _context.Scores.FindAsync(id);
            if (scores == null)
            {
                return NotFound();
            }

            _context.Scores.Remove(scores);
            await _context.SaveChangesAsync();

            return scores;
        }

        private bool ScoresExists(int id)
        {
            return _context.Scores.Any(e => e.ScoreId == id);
        }
    }
}
