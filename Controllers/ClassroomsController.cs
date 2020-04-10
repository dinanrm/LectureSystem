using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LectureSystem.Data;
using LectureSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace LectureSystem.Controllers
{
    [Authorize]
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
        /// <summary>
        /// Get all classrooms
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/classrooms
        ///
        /// </remarks>
        /// <response code="200">Returns all of classroom entity.</response>
        /// <response code="401">User is unauthorized</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classrooms>>> GetClassrooms()
        {
            return await _context.Classrooms.ToListAsync();
        }

        // GET: api/Classrooms/5
        /// <summary>
        /// Get a classroom by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/classrooms/1
        ///
        /// </remarks>
        /// <param name="id">A classroom id</param>
        /// <response code="200">Returns a classroom entity.</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="404">If the id of classroom entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
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
        /// <summary>
        /// Update a classroom by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/classrooms/1
        ///     {
        ///         "classroomId" : 1,
        ///         "name": "A1",
        ///         "description": "First classroom"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">A classroom id</param>
        /// <param name="classrooms">A classroom entity</param>
        /// <response code="204">Returns updated classroom entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="404">If the id of classroom entity is not exist</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
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
        /// <summary>
        /// Add a new classroom
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/classrooms
        ///     {
        ///         "name": "A1",
        ///         "description": "First classroom"
        ///     }
        ///
        /// </remarks>
        /// <param name="classrooms">A classroom entity</param>
        /// <response code="201">Returns the created classroom entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="401">User is unauthorized</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpPost]
        public async Task<ActionResult<Classrooms>> PostClassrooms(Classrooms classrooms)
        {
            _context.Classrooms.Add(classrooms);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClassrooms", new { id = classrooms.ClassroomId }, classrooms);
        }

        // DELETE: api/Classrooms/5
        /// <summary>
        /// Delete a classroom by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/classrooms/1
        ///
        /// </remarks>
        /// <param name="id">A classroom id</param>
        /// <response code="200">Returns deleted classroom entity.</response>
        /// <response code="401">User is unauthorized</response>
        /// <response code="404">If the id of classroom entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
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
