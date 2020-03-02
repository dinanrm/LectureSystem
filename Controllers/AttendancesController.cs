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
        /// <summary>
        /// Get all attendances
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/attendances
        ///
        /// </remarks>
        /// <response code="200">Returns all of attendance entity.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendances>>> GetAttendances()
        {
            return await _context.Attendances.ToListAsync();
        }

        // GET: api/Attendances/5
        /// <summary>
        /// Get a attendance by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/attendances/1
        ///
        /// </remarks>
        /// <param name="id">A attendance id</param>
        /// <response code="200">Returns a attendance entity.</response>
        /// <response code="404">If the id of attendance entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
        /// <summary>
        /// Update a attendance by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/attendances/1
        ///
        /// </remarks>
        /// <param name="id">A attendance id</param>
        /// <param name="attendances">A attendance entity</param>
        /// <response code="204">Returns updated attendance entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="404">If the id of attendance entity is not exist</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
        /// <summary>
        /// Add a new attendance
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/attendances
        ///
        /// </remarks>
        /// <param name="attendances">A attendance entity</param>
        /// <response code="201">Returns the created attendance entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult<Attendances>> PostAttendances(Attendances attendances)
        {
            _context.Attendances.Add(attendances);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendances", new { id = attendances.AttendId }, attendances);
        }

        // DELETE: api/Attendances/5
        /// <summary>
        /// Delete a attendance by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/attendances/1
        ///
        /// </remarks>
        /// <param name="id">A attendance id</param>
        /// <response code="200">Returns deleted attendance entity.</response>
        /// <response code="404">If the id of attendance entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
