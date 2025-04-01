using Microsoft.AspNetCore.Mvc;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDotNetBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectStructuresController : ControllerBase
    {
        private readonly IProjectStructureService _projectStructureService;

        public ProjectStructuresController(IProjectStructureService projectStructureService)
        {
            _projectStructureService = projectStructureService ?? throw new ArgumentNullException(nameof(projectStructureService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectStructureDto>>> GetProjectStructures()
        {
            var projectStructures = await _projectStructureService.GetAllProjectStructuresAsync();
            return Ok(projectStructures);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectStructureDto>> GetProjectStructure(string id)
        {
            try
            {
                var projectStructure = await _projectStructureService.GetProjectStructureByIdAsync(id);
                if (projectStructure == null)
                {
                    return NotFound();
                }
                return Ok(projectStructure);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<ProjectStructureDto>>> GetProjectStructuresByProject(string projectId)
        {
            try
            {
                var projectStructures = await _projectStructureService.GetProjectStructuresByProjectIdAsync(projectId);
                return Ok(projectStructures);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProjectStructureDto>> CreateProjectStructure([FromBody] CreateProjectStructureDto createProjectStructureDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdProjectStructure = await _projectStructureService.CreateProjectStructureAsync(createProjectStructureDto);
                return CreatedAtAction(nameof(GetProjectStructure), new { id = createdProjectStructure.Id }, createdProjectStructure);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectStructure(string id)
        {
            try
            {
                await _projectStructureService.DeleteProjectStructureAsync(id);
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