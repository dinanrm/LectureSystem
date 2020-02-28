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
    public class CourseScoresController : ControllerBase
    {
        private readonly LectureSystemDbContext _context;

        public CourseScoresController(LectureSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/CourseScores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseScores>>> GetCourseScores()
        {
            return await _context.CourseScores.ToListAsync();
        }

        // GET: api/CourseScores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseScores>> GetCourseScores(int id)
        {
            var courseScores = await _context.CourseScores.FindAsync(id);

            if (courseScores == null)
            {
                return NotFound();
            }

            return courseScores;
        }

        // PUT: api/CourseScores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseScores(int id, CourseScores courseScores)
        {
            if (id != courseScores.CourseScoreId)
            {
                return BadRequest();
            }

            _context.Entry(courseScores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseScoresExists(id))
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

        // POST: api/CourseScores
        [HttpPost]
        public async Task<ActionResult<CourseScores>> PostCourseScores(CourseScores courseScores)
        {
            _context.CourseScores.Add(courseScores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourseScores", new { id = courseScores.CourseScoreId }, courseScores);
        }

        // DELETE: api/CourseScores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseScores>> DeleteCourseScores(int id)
        {
            var courseScores = await _context.CourseScores.FindAsync(id);
            if (courseScores == null)
            {
                return NotFound();
            }

            _context.CourseScores.Remove(courseScores);
            await _context.SaveChangesAsync();

            return courseScores;
        }

        private bool CourseScoresExists(int id)
        {
            return _context.CourseScores.Any(e => e.CourseScoreId == id);
        }
    }
}
