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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teaches>>> GetTeaches()
        {
            return await _context.Teaches.ToListAsync();
        }

        // GET: api/Teaches/5
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
        [HttpPost]
        public async Task<ActionResult<Teaches>> PostTeaches(Teaches teaches)
        {
            _context.Teaches.Add(teaches);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeaches", new { id = teaches.TeachId }, teaches);
        }

        // DELETE: api/Teaches/5
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
