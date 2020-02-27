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
    public class AttendancesController : ControllerBase
    {
        private readonly LectureSystemDbContext _context;

        public AttendancesController(LectureSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/Attendances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendances>>> GetAttendances()
        {
            return await _context.Attendances.ToListAsync();
        }

        // GET: api/Attendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendances>> GetAttendances(int id)
        {
            var attendances = await _context.Attendances.FindAsync(id);

            if (attendances == null)
            {
                return NotFound();
            }

            return attendances;
        }

        // PUT: api/Attendances/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendances(int id, Attendances attendances)
        {
            if (id != attendances.AttendId)
            {
                return BadRequest();
            }

            _context.Entry(attendances).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendancesExists(id))
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

        // POST: api/Attendances
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Attendances>> PostAttendances(Attendances attendances)
        {
            _context.Attendances.Add(attendances);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendances", new { id = attendances.AttendId }, attendances);
        }

        // DELETE: api/Attendances/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Attendances>> DeleteAttendances(int id)
        {
            var attendances = await _context.Attendances.FindAsync(id);
            if (attendances == null)
            {
                return NotFound();
            }

            _context.Attendances.Remove(attendances);
            await _context.SaveChangesAsync();

            return attendances;
        }

        private bool AttendancesExists(int id)
        {
            return _context.Attendances.Any(e => e.AttendId == id);
        }
    }
}
