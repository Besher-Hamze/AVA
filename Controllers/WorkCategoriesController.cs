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
    public class WorkCategoriesController : ControllerBase
    {
        private readonly IWorkCategoryService _workCategoryService;

        public WorkCategoriesController(IWorkCategoryService workCategoryService)
        {
            _workCategoryService = workCategoryService ?? throw new ArgumentNullException(nameof(workCategoryService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkCategoryDto>>> GetWorkCategories()
        {
            var workCategories = await _workCategoryService.GetAllWorkCategoriesAsync();
            return Ok(workCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkCategoryDto>> GetWorkCategory(string id)
        {
            try
            {
                var workCategory = await _workCategoryService.GetWorkCategoryByIdAsync(id);
                if (workCategory == null)
                {
                    return NotFound();
                }
                return Ok(workCategory);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<WorkCategoryDto>> CreateWorkCategory([FromBody] CreateWorkCategoryDto createWorkCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdWorkCategory = await _workCategoryService.CreateWorkCategoryAsync(createWorkCategoryDto);
                return CreatedAtAction(nameof(GetWorkCategory), new { id = createdWorkCategory.Id }, createdWorkCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkCategory(string id)
        {
            try
            {
                await _workCategoryService.DeleteWorkCategoryAsync(id);
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