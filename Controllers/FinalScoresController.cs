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
    public class FinalScoresController : ControllerBase
    {
        private readonly LectureSystemDbContext _context;

        public FinalScoresController(LectureSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/FinalScores
        /// <summary>
        /// Get all finalScores
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/finalScores
        ///
        /// </remarks>
        /// <response code="200">Returns all of finalScore entity.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinalScores>>> GetFinalScores()
        {
            return await _context.FinalScores.ToListAsync();
        }

        // GET: api/FinalScores/5
        /// <summary>
        /// Get a finalScore by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/finalScores/1
        ///
        /// </remarks>
        /// <param name="id">A finalScore id</param>
        /// <response code="200">Returns a finalScore entity.</response>
        /// <response code="404">If the id of finalScore entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<FinalScores>> GetFinalScores(int id)
        {
            var finalScores = await _context.FinalScores
                .Where(fs => fs.FinalScoreId == id)
                .Include(x => x.Student)
                .Include(x => x.Semester)
                .FirstOrDefaultAsync();

            if (finalScores == null)
            {
                return NotFound();
            }

            return finalScores;
        }

        // PUT: api/FinalScores/5
        /// <summary>
        /// Update a finalScore by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/finalScores/1
        ///
        /// </remarks>
        /// <param name="id">A finalScore id</param>
        /// <param name="finalScores">A finalScore entity</param>
        /// <response code="204">Returns updated finalScore entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="404">If the id of finalScore entity is not exist</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinalScores(int id, FinalScores finalScores)
        {
            if (id != finalScores.FinalScoreId)
            {
                return BadRequest();
            }

            _context.Entry(finalScores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinalScoresExists(id))
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

        // POST: api/FinalScores
        /// <summary>
        /// Add a new finalScore
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/finalScores
        ///
        /// </remarks>
        /// <param name="finalScores">A finalScore entity</param>
        /// <response code="201">Returns the created finalScore entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult<FinalScores>> PostFinalScores(FinalScores finalScores)
        {
            _context.FinalScores.Add(finalScores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFinalScores", new { id = finalScores.FinalScoreId }, finalScores);
        }

        // DELETE: api/FinalScores/5
        /// <summary>
        /// Delete a finalScore by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/finalScores/1
        ///
        /// </remarks>
        /// <param name="id">A finalScore id</param>
        /// <response code="200">Returns deleted finalScore entity.</response>
        /// <response code="404">If the id of finalScore entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<FinalScores>> DeleteFinalScores(int id)
        {
            var finalScores = await _context.FinalScores
                .Where(fs => fs.FinalScoreId == id)
                .Include(x => x.Student)
                .Include(x => x.Semester)
                .FirstOrDefaultAsync();

            if (finalScores == null)
            {
                return NotFound();
            }

            _context.FinalScores.Remove(finalScores);
            await _context.SaveChangesAsync();

            return finalScores;
        }

        private bool FinalScoresExists(int id)
        {
            return _context.FinalScores.Any(e => e.FinalScoreId == id);
        }
    }
}
