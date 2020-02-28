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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinalScores>>> GetFinalScores()
        {
            return await _context.FinalScores.ToListAsync();
        }

        // GET: api/FinalScores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FinalScores>> GetFinalScores(int id)
        {
            var finalScores = await _context.FinalScores.FindAsync(id);

            if (finalScores == null)
            {
                return NotFound();
            }

            return finalScores;
        }

        // PUT: api/FinalScores/5
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
        [HttpPost]
        public async Task<ActionResult<FinalScores>> PostFinalScores(FinalScores finalScores)
        {
            _context.FinalScores.Add(finalScores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFinalScores", new { id = finalScores.FinalScoreId }, finalScores);
        }

        // DELETE: api/FinalScores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FinalScores>> DeleteFinalScores(int id)
        {
            var finalScores = await _context.FinalScores.FindAsync(id);
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
