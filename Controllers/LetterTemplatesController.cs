using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Services;

namespace MongoDotNetBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class LetterTemplatesController : ControllerBase
    {
        private readonly ILetterTemplateService _letterTemplateService;

        public LetterTemplatesController(ILetterTemplateService letterTemplateService)
        {
            _letterTemplateService = letterTemplateService ?? throw new ArgumentNullException(nameof(letterTemplateService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LetterTemplateDto>>> GetLetterTemplates()
        {
            var letterTemplates = await _letterTemplateService.GetAllLetterTemplatesAsync();
            return Ok(letterTemplates);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LetterTemplateDto>> GetLetterTemplate(string id)
        {
            try
            {
                var letterTemplate = await _letterTemplateService.GetLetterTemplateByIdAsync(id);
                return Ok(letterTemplate);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<LetterTemplateDto>>> SearchLetterTemplates([FromQuery] string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return BadRequest("Search term cannot be empty");
            }
            
            var letterTemplates = await _letterTemplateService.SearchLetterTemplatesByTitleAsync(term);
            return Ok(letterTemplates);
        }

        [HttpPost]
        public async Task<ActionResult<LetterTemplateDto>> CreateLetterTemplate([FromBody] CreateLetterTemplateDto createLetterTemplateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdLetterTemplate = await _letterTemplateService.CreateLetterTemplateAsync(createLetterTemplateDto);
                return CreatedAtAction(nameof(GetLetterTemplate), new { id = createdLetterTemplate.Id }, createdLetterTemplate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLetterTemplate(string id, [FromBody] UpdateLetterTemplateDto updateLetterTemplateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _letterTemplateService.UpdateLetterTemplateAsync(id, updateLetterTemplateDto);
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
        public async Task<IActionResult> DeleteLetterTemplate(string id)
        {
            try
            {
                await _letterTemplateService.DeleteLetterTemplateAsync(id);
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
