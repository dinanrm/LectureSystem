using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LectureSystem.Data;
using LectureSystem.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace LectureSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly LectureSystemDbContext _context;
        private readonly IWebHostEnvironment he;

        public FilesController(LectureSystemDbContext context, IWebHostEnvironment e)
        {
            _context = context;
            he = e;
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
            var files = await _context.Files
                .Where(f => f.FileId == id)
                .Include(x => x.Course)
                .FirstOrDefaultAsync();

            if (files == null)
            {
                return NotFound();
            }

            return files;
        }

        // POST: api/Files/Upload
        /// <summary>
        /// Upload files
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/files/upload
        ///
        /// </remarks>
        /// <param name="files">A files that will be upload</param>
        /// <param name="CourseId">A course id</param>
        /// <param name="Description">A file description</param>
        /// <param name="Author">The author of file</param>
        /// <response code="201">Returns the created file entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(List<IFormFile> files, [FromForm] int? CourseId, [FromForm] string Description, [FromForm] string Author )
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("Files not selected");
            }

            if (CourseId == null)
            {
                return BadRequest("Course id is empty");
            }

            List<Files> listOfFiles = new List<Files>();

            foreach (var formFile in files)
            {
                var uniqueName = GetUniqueFileName(formFile.FileName);

                var filePath = Path.Combine(he.WebRootPath, "files", uniqueName);

                var size = formFile.Length;

                Files file = new Files
                {
                    CourseId = CourseId,
                    Name = uniqueName,
                    Description = Description,
                    Author = Author,
                    Type = GetContentType(filePath),
                    Size = size,
                    Status = true,
                };

                if (formFile.Length > 0)
                {
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await formFile.CopyToAsync(stream);
                }

                file.FilePath = Encoding.ASCII.GetBytes(filePath);

                listOfFiles.Add(file);

                _context.Files.Add(file);
            }
            await _context.SaveChangesAsync();

            return Ok(listOfFiles);
        }

        // GET: api/Files/Download
        /// <summary>
        /// Download a file
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/files/download/1
        ///
        /// </remarks>
        /// <param name="id">A file id</param>
        /// <response code="200">Returns the file.</response>
        /// <response code="404">If the id of file entity is not exist</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("Download/{id}")]
        public async Task<ActionResult<Files>> Download(int id)
        {
            var file = await _context.Files.FindAsync(id);

            if (file == null)
            {
                return NotFound();
            }

            var filename = file.Name;

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/files", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            Response.Headers.Add("Filename", filename);

            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        // PUT: api/Documents/1
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
        /// <param name="fileId">A file id</param>
        /// <param name="files">Files that will be upload</param>
        /// <param name="CourseId">A course id</param>
        /// <param name="Description">A file description</param>
        /// <param name="Author">The author of file</param>
        /// <response code="204">Returns updated file entity.</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax</response>
        /// <response code="404">If the id of file entity is not exist</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, List<IFormFile> files, [FromForm] int fileId, [FromForm] int? CourseId, [FromForm] string Description, [FromForm] string Author)
        {
            Files file = _context.Files.Find(id);

            if (id != fileId)
            {
                return BadRequest();
            }

            if (file == null)
            {
                return NotFound();
            }
            if (CourseId != null)
            {
                file.CourseId = CourseId;
            }
            if (Description != null)
            {
                file.Description = Description;
            }
            if (Author != null)
            {
                file.Author = Author;
            }

            if (files != null)
            {
                foreach (var formFile in files)
                {

                    file.Name = GetUniqueFileName(formFile.FileName);

                    var filePath = Path.Combine(he.WebRootPath, "files", file.Name);

                    if (formFile.Length > 0)
                        using (var stream = new FileStream(filePath, FileMode.Create))
                            await formFile.CopyToAsync(stream);

                    file.FilePath = Encoding.ASCII.GetBytes(filePath);
                }
            }

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

            return Ok(file);
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
            var files = await _context.Files
                .Where(f => f.FileId == id)
                .Include(x => x.Course)
                .FirstOrDefaultAsync();

            if (files == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(he.WebRootPath, "files", files.Name);
            System.IO.File.Delete(filePath);

            _context.Files.Remove(files);
            await _context.SaveChangesAsync();

            return files;
        }

        private bool FilesExists(int id)
        {
            return _context.Files.Any(e => e.FileId == id);
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 7)
                      + Path.GetExtension(fileName);
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".ppt", "application/vnd.ms-powerpoint" },
                {".pptx","application/vnd.openxmlformats-officedocument.presentationml.presentation"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
