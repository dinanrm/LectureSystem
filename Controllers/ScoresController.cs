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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Scores>>> GetScores()
        {
            return await _context.Scores.ToListAsync();
        }

        // GET: api/Scores/5
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
        [HttpPost]
        public async Task<ActionResult<Scores>> PostScores(Scores scores)
        {
            _context.Scores.Add(scores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScores", new { id = scores.ScoreId }, scores);
        }

        // DELETE: api/Scores/5
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
