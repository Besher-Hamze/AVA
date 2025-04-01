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
    public class WorkTypesController : ControllerBase
    {
        private readonly IWorkTypeService _workTypeService;

        public WorkTypesController(IWorkTypeService workTypeService)
        {
            _workTypeService = workTypeService ?? throw new ArgumentNullException(nameof(workTypeService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkTypeDto>>> GetWorkTypes()
        {
            var workTypes = await _workTypeService.GetAllWorkTypesAsync();
            return Ok(workTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkTypeDto>> GetWorkType(string id)
        {
            try
            {
                var workType = await _workTypeService.GetWorkTypeByIdAsync(id);
                if (workType == null)
                {
                    return NotFound();
                }
                return Ok(workType);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<WorkTypeDto>>> GetWorkTypesByCategory(string categoryId)
        {
            try
            {
                var workTypes = await _workTypeService.GetWorkTypesByWorkCategoryIdAsync(categoryId);
                return Ok(workTypes);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<WorkTypeDto>> CreateWorkType([FromBody] CreateWorkTypeDto createWorkTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdWorkType = await _workTypeService.CreateWorkTypeAsync(createWorkTypeDto);
                return CreatedAtAction(nameof(GetWorkType), new { id = createdWorkType.Id }, createdWorkType);
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
        public async Task<IActionResult> DeleteWorkType(string id)
        {
            try
            {
                await _workTypeService.DeleteWorkTypeAsync(id);
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