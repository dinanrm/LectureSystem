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
        /// <summary>
        /// Get all classSchedules
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/classSchedules
        ///
        /// </remarks>
        /// <response code="200">Returns all of classSchedule entity.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassSchedules>>> GetClassSchedules()
        {
            return await _context.ClassSchedules.ToListAsync();
        }

        // GET: api/ClassSchedules/5
        /// <summary>
        /// Get a classSchedule by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/classSchedules/1
        ///
        /// </remarks>
        /// <param name="id">A classSchedule id</param>
        /// <response code="200">Returns a classSchedule entity.</response>
        /// <response code="404">If the id of classSchedule entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
        /// <summary>
        /// Update a classSchedule by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/classSchedules/1
        ///     {
        ///         "classScheduleId": 1,
        ///         "courseId": 3,
        ///         "classroomId": 3,
        ///         "day": "Tuesday",
        ///         "startTime": "2020-03-03T07:04:04.540Z",
        ///         "endTime": "2020-03-03T07:04:04.540Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">A classSchedule id</param>
        /// <param name="classSchedules">A classSchedule entity</param>
        /// <response code="204">Returns updated classSchedule entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="404">If the id of classSchedule entity is not exist</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
        /// <summary>
        /// Add a new classSchedule
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/classSchedules
        ///     {
        ///         "courseId": 3,
        ///         "classroomId": 3,
        ///         "day": "Tuesday",
        ///         "startTime": "2020-03-03T07:04:04.540Z",
        ///         "endTime": "2020-03-03T07:04:04.540Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="classSchedules">A classSchedule entity</param>
        /// <response code="201">Returns the created classSchedule entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult<ClassSchedules>> PostClassSchedules(ClassSchedules classSchedules)
        {
            _context.ClassSchedules.Add(classSchedules);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClassSchedules", new { id = classSchedules.ClassScheduleId }, classSchedules);
        }

        // DELETE: api/ClassSchedules/5
        /// <summary>
        /// Delete a classSchedule by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/classSchedules/1
        ///
        /// </remarks>
        /// <param name="id">A classSchedule id</param>
        /// <response code="200">Returns deleted classSchedule entity.</response>
        /// <response code="404">If the id of classSchedule entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
