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
    public class ClassSchedulesController : ControllerBase
    {
        private readonly LectureSystemDbContext _context;

        public ClassSchedulesController(LectureSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/ClassSchedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassSchedules>>> GetClassSchedules()
        {
            return await _context.ClassSchedules.ToListAsync();
        }

        // GET: api/ClassSchedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassSchedules>> GetClassSchedules(int id)
        {
            var classSchedules = await _context.ClassSchedules.FindAsync(id);

            if (classSchedules == null)
            {
                return NotFound();
            }

            return classSchedules;
        }

        // PUT: api/ClassSchedules/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassSchedules(int id, ClassSchedules classSchedules)
        {
            if (id != classSchedules.ClassScheduleId)
            {
                return BadRequest();
            }

            _context.Entry(classSchedules).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassSchedulesExists(id))
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

        // POST: api/ClassSchedules
        [HttpPost]
        public async Task<ActionResult<ClassSchedules>> PostClassSchedules(ClassSchedules classSchedules)
        {
            _context.ClassSchedules.Add(classSchedules);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClassSchedules", new { id = classSchedules.ClassScheduleId }, classSchedules);
        }

        // DELETE: api/ClassSchedules/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClassSchedules>> DeleteClassSchedules(int id)
        {
            var classSchedules = await _context.ClassSchedules.FindAsync(id);
            if (classSchedules == null)
            {
                return NotFound();
            }

            _context.ClassSchedules.Remove(classSchedules);
            await _context.SaveChangesAsync();

            return classSchedules;
        }

        private bool ClassSchedulesExists(int id)
        {
            return _context.ClassSchedules.Any(e => e.ClassScheduleId == id);
        }
    }
}
