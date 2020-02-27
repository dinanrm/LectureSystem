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
    public class ClassroomsController : ControllerBase
    {
        private readonly LectureSystemDbContext _context;

        public ClassroomsController(LectureSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/Classrooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classrooms>>> GetClassrooms()
        {
            return await _context.Classrooms.ToListAsync();
        }

        // GET: api/Classrooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Classrooms>> GetClassrooms(int id)
        {
            var classrooms = await _context.Classrooms.FindAsync(id);

            if (classrooms == null)
            {
                return NotFound();
            }

            return classrooms;
        }

        // PUT: api/Classrooms/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassrooms(int id, Classrooms classrooms)
        {
            if (id != classrooms.ClassroomId)
            {
                return BadRequest();
            }

            _context.Entry(classrooms).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassroomsExists(id))
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

        // POST: api/Classrooms
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Classrooms>> PostClassrooms(Classrooms classrooms)
        {
            _context.Classrooms.Add(classrooms);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClassrooms", new { id = classrooms.ClassroomId }, classrooms);
        }

        // DELETE: api/Classrooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Classrooms>> DeleteClassrooms(int id)
        {
            var classrooms = await _context.Classrooms.FindAsync(id);
            if (classrooms == null)
            {
                return NotFound();
            }

            _context.Classrooms.Remove(classrooms);
            await _context.SaveChangesAsync();

            return classrooms;
        }

        private bool ClassroomsExists(int id)
        {
            return _context.Classrooms.Any(e => e.ClassroomId == id);
        }
    }
}
