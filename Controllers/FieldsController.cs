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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fields>>> GetFields()
        {
            return await _context.Fields.ToListAsync();
        }

        // GET: api/Fields/5
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Fields>> PostFields(Fields fields)
        {
            _context.Fields.Add(fields);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFields", new { id = fields.FieldId }, fields);
        }

        // DELETE: api/Fields/5
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
