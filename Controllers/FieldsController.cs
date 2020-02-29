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
    public class FieldsController : ControllerBase
    {
        private readonly LectureSystemDbContext _context;

        public FieldsController(LectureSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/Fields
        /// <summary>
        /// Get all fields
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/fields
        ///
        /// </remarks>
        /// <response code="200">Returns all of field entity.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fields>>> GetFields()
        {
            return await _context.Fields.ToListAsync();
        }

        // GET: api/Fields/5
        /// <summary>
        /// Get a field by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/fields/1
        ///
        /// </remarks>
        /// <param name="id">A field id</param>
        /// <response code="200">Returns a field entity.</response>
        /// <response code="404">If the id of field entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Fields>> GetFields(int id)
        {
            var fields = await _context.Fields.FindAsync(id);

            if (fields == null)
            {
                return NotFound();
            }

            return fields;
        }

        // PUT: api/Fields/5
        /// <summary>
        /// Update a field by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/fields/1
        ///
        /// </remarks>
        /// <param name="id">A field id</param>
        /// <param name="fields">A field entity</param>
        /// <response code="204">Returns updated field entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="404">If the id of field entity is not exist</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFields(int id, Fields fields)
        {
            if (id != fields.FieldId)
            {
                return BadRequest();
            }

            _context.Entry(fields).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FieldsExists(id))
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

        // POST: api/Fields
        /// <summary>
        /// Add a new field
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/fields
        ///
        /// </remarks>
        /// <param name="fields">A field entity</param>
        /// <response code="201">Returns the created field entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult<Fields>> PostFields(Fields fields)
        {
            _context.Fields.Add(fields);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFields", new { id = fields.FieldId }, fields);
        }

        // DELETE: api/Fields/5
        /// <summary>
        /// Delete a field by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/fields/1
        ///
        /// </remarks>
        /// <param name="id">A field id</param>
        /// <response code="200">Returns deleted field entity.</response>
        /// <response code="404">If the id of field entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fields>> DeleteFields(int id)
        {
            var fields = await _context.Fields.FindAsync(id);
            if (fields == null)
            {
                return NotFound();
            }

            _context.Fields.Remove(fields);
            await _context.SaveChangesAsync();

            return fields;
        }

        private bool FieldsExists(int id)
        {
            return _context.Fields.Any(e => e.FieldId == id);
        }
    }
}
