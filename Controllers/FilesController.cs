using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MongoDotNetBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly string _uploadsFolder;

        public FilesController(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
            _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileStorageDto>>> GetAllFiles()
        {
            var files = await _fileStorageService.GetAllFilesAsync();
            return Ok(files);
        }

        [HttpGet("info/{id}")]
        public async Task<ActionResult<FileStorageDto>> GetFile(string id)
        {
            try
            {
                var file = await _fileStorageService.GetFileByIdAsync(id);
                if (file == null)
                {
                    return NotFound();
                }
                return Ok(file);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("download/{fileName}")]
        public IActionResult DownloadFile(string fileName)
        {
            var filePath = Path.Combine(_uploadsFolder, fileName);
            
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, contentType, Path.GetFileName(filePath));
        }

        [HttpPost("upload")]
        public async Task<ActionResult<FileStorageDto>> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            try
            {
                var uploadedFile = await _fileStorageService.UploadFileAsync(file);
                return CreatedAtAction(nameof(GetFile), new { id = uploadedFile.Id }, uploadedFile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(string id)
        {
            try
            {
                await _fileStorageService.DeleteFileAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}