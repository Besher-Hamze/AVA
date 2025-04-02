using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Services;

namespace MongoDotNetBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FoldersController : ControllerBase
    {
        private readonly IFolderService _folderService;

        public FoldersController(IFolderService folderService)
        {
            _folderService = folderService ?? throw new ArgumentNullException(nameof(folderService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FolderDto>>> GetFolders()
        {
            var folders = await _folderService.GetAllFoldersAsync();
            return Ok(folders);
        }

        [HttpGet("root")]
        public async Task<ActionResult<IEnumerable<FolderDto>>> GetRootFolders()
        {
            var folders = await _folderService.GetRootFoldersAsync();
            return Ok(folders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FolderDto>> GetFolder(string id)
        {
            try
            {
                var folder = await _folderService.GetFolderByIdAsync(id);
                return Ok(folder);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/contents")]
        public async Task<ActionResult<FolderDto>> GetFolderWithContents(string id)
        {
            try
            {
                var folder = await _folderService.GetFolderWithContentsAsync(id);
                return Ok(folder);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/subfolders")]
        public async Task<ActionResult<IEnumerable<FolderDto>>> GetSubFolders(string id)
        {
            try
            {
                var folders = await _folderService.GetSubFoldersAsync(id);
                return Ok(folders);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<FolderDto>> CreateFolder([FromBody] CreateFolderDto createFolderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdFolder = await _folderService.CreateFolderAsync(createFolderDto);
                return CreatedAtAction(nameof(GetFolder), new { id = createdFolder.Id }, createdFolder);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFolder(string id, [FromBody] UpdateFolderDto updateFolderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _folderService.UpdateFolderAsync(id, updateFolderDto);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFolder(string id)
        {
            try
            {
                await _folderService.DeleteFolderAsync(id);
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
