using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Services;

namespace MongoDotNetBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PlansController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlansController(IPlanService planService)
        {
            _planService = planService ?? throw new ArgumentNullException(nameof(planService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanDto>>> GetPlans()
        {
            var plans = await _planService.GetAllPlansAsync();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlanDto>> GetPlan(string id)
        {
            try
            {
                var plan = await _planService.GetPlanByIdAsync(id);
                return Ok(plan);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("folder/{folderId}")]
        public async Task<ActionResult<IEnumerable<PlanDto>>> GetPlansByFolder(string folderId)
        {
            try
            {
                var plans = await _planService.GetPlansByFolderIdAsync(folderId);
                return Ok(plans);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("type/{type}")]
        public async Task<ActionResult<IEnumerable<PlanDto>>> GetPlansByType(string type)
        {
            var plans = await _planService.GetPlansByTypeAsync(type);
            return Ok(plans);
        }

        [HttpPost]
        public async Task<ActionResult<PlanDto>> CreatePlan([FromBody] CreatePlanDto createPlanDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdPlan = await _planService.CreatePlanAsync(createPlanDto);
                return CreatedAtAction(nameof(GetPlan), new { id = createdPlan.Id }, createdPlan);
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
        public async Task<IActionResult> UpdatePlan(string id, [FromBody] UpdatePlanDto updatePlanDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _planService.UpdatePlanAsync(id, updatePlanDto);
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
        public async Task<IActionResult> DeletePlan(string id)
        {
            try
            {
                await _planService.DeletePlanAsync(id);
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
