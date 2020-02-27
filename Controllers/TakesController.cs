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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Takes>>> GetTakes()
        {
            return await _context.Takes.ToListAsync();
        }

        // GET: api/Takes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Takes>> GetTakes(int id)
        {
            var takes = await _context.Takes.FindAsync(id);

            if (takes == null)
            {
                return NotFound();
            }

            return takes;
        }

        // PUT: api/Takes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Takes>> PostTakes(Takes takes)
        {
            _context.Takes.Add(takes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTakes", new { id = takes.TakeId }, takes);
        }

        // DELETE: api/Takes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Takes>> DeleteTakes(int id)
        {
            var takes = await _context.Takes.FindAsync(id);
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
