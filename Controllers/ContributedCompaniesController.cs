using Microsoft.AspNetCore.Mvc;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Services;

namespace MongoDotNetBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContributedCompaniesController : ControllerBase
    {
        private readonly IContributedCompanyService _contributedCompanyService;

        public ContributedCompaniesController(IContributedCompanyService contributedCompanyService)
        {
            _contributedCompanyService = contributedCompanyService ?? throw new ArgumentNullException(nameof(contributedCompanyService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContributedCompanyDto>>> GetContributedCompanies()
        {
            var contributedCompanies = await _contributedCompanyService.GetAllContributedCompaniesAsync();
            return Ok(contributedCompanies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContributedCompanyDto>> GetContributedCompany(string id)
        {
            try
            {
                var contributedCompany = await _contributedCompanyService.GetContributedCompanyByIdAsync(id);
                if (contributedCompany == null)
                {
                    return NotFound();
                }
                return Ok(contributedCompany);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<ContributedCompanyDto>>> GetContributedCompaniesByProject(string projectId)
        {
            try
            {
                var contributedCompanies = await _contributedCompanyService.GetContributedCompaniesByProjectIdAsync(projectId);
                return Ok(contributedCompanies);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("company/{companyId}")]
        public async Task<ActionResult<IEnumerable<ContributedCompanyDto>>> GetProjectsByCompany(string companyId)
        {
            try
            {
                var contributedCompanies = await _contributedCompanyService.GetProjectsByCompanyIdAsync(companyId);
                return Ok(contributedCompanies);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ContributedCompanyDto>> CreateContributedCompany([FromBody] CreateContributedCompanyDto createContributedCompanyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdContributedCompany = await _contributedCompanyService.CreateContributedCompanyAsync(createContributedCompanyDto);
                return CreatedAtAction(nameof(GetContributedCompany), new { id = createdContributedCompany.Id }, createdContributedCompany);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContributedCompany(string id)
        {
            try
            {
                await _contributedCompanyService.DeleteContributedCompanyAsync(id);
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