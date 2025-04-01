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
    public class ProjectUnitsController : ControllerBase
    {
        private readonly IProjectUnitService _projectUnitService;

        public ProjectUnitsController(IProjectUnitService projectUnitService)
        {
            _projectUnitService = projectUnitService ?? throw new ArgumentNullException(nameof(projectUnitService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectUnitDto>>> GetProjectUnits()
        {
            var projectUnits = await _projectUnitService.GetAllProjectUnitsAsync();
            return Ok(projectUnits);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectUnitDto>> GetProjectUnit(string id)
        {
            try
            {
                var projectUnit = await _projectUnitService.GetProjectUnitByIdAsync(id);
                if (projectUnit == null)
                {
                    return NotFound();
                }
                return Ok(projectUnit);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<ProjectUnitDto>>> GetProjectUnitsByProject(string projectId)
        {
            try
            {
                var projectUnits = await _projectUnitService.GetProjectUnitsByProjectIdAsync(projectId);
                return Ok(projectUnits);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProjectUnitDto>> CreateProjectUnit([FromBody] CreateProjectUnitDto createProjectUnitDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdProjectUnit = await _projectUnitService.CreateProjectUnitAsync(createProjectUnitDto);
                return CreatedAtAction(nameof(GetProjectUnit), new { id = createdProjectUnit.Id }, createdProjectUnit);
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
        public async Task<IActionResult> DeleteProjectUnit(string id)
        {
            try
            {
                await _projectUnitService.DeleteProjectUnitAsync(id);
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