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
    public class FilesController : ControllerBase
    {
        private readonly LectureSystemDbContext _context;

        public FilesController(LectureSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/Files
        /// <summary>
        /// Get all files
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/files
        ///
        /// </remarks>
        /// <response code="200">Returns all of file entity.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Files>>> GetFiles()
        {
            return await _context.Files.ToListAsync();
        }

        // GET: api/Files/5
        /// <summary>
        /// Get a file by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/files/1
        ///
        /// </remarks>
        /// <param name="id">A file id</param>
        /// <response code="200">Returns a file entity.</response>
        /// <response code="404">If the id of file entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Files>> GetFiles(int id)
        {
            var files = await _context.Files.FindAsync(id);

            if (files == null)
            {
                return NotFound();
            }

            return files;
        }

        // PUT: api/Files/5
        /// <summary>
        /// Update a file by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/files/1
        ///
        /// </remarks>
        /// <param name="id">A file id</param>
        /// <param name="files">A file entity</param>
        /// <response code="204">Returns updated file entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="404">If the id of file entity is not exist</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFiles(int id, Files files)
        {
            if (id != files.FileId)
            {
                return BadRequest();
            }

            _context.Entry(files).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilesExists(id))
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

        // POST: api/Files
        /// <summary>
        /// Add a new file
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/files
        ///
        /// </remarks>
        /// <param name="files">A file entity</param>
        /// <response code="201">Returns the created file entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult<Files>> PostFiles(Files files)
        {
            _context.Files.Add(files);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFiles", new { id = files.FileId }, files);
        }

        // DELETE: api/Files/5
        /// <summary>
        /// Delete a file by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/files/1
        ///
        /// </remarks>
        /// <param name="id">A file id</param>
        /// <response code="200">Returns deleted file entity.</response>
        /// <response code="404">If the id of file entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Files>> DeleteFiles(int id)
        {
            var files = await _context.Files.FindAsync(id);
            if (files == null)
            {
                return NotFound();
            }

            _context.Files.Remove(files);
            await _context.SaveChangesAsync();

            return files;
        }

        private bool FilesExists(int id)
        {
            return _context.Files.Any(e => e.FileId == id);
        }
    }
}
