using AutoMapper;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDotNetBackend.Services
{
    public class ContributedCompanyService : IContributedCompanyService
    {
        private readonly IContributedCompanyRepository _contributedCompanyRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ContributedCompanyService(
            IContributedCompanyRepository contributedCompanyRepository,
            ICompanyRepository companyRepository,
            IProjectRepository projectRepository,
            IMapper mapper)
        {
            _contributedCompanyRepository = contributedCompanyRepository ?? throw new ArgumentNullException(nameof(contributedCompanyRepository));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ContributedCompanyDto>> GetAllContributedCompaniesAsync()
        {
            var contributedCompanies = await _contributedCompanyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ContributedCompanyDto>>(contributedCompanies);
        }

        public async Task<ContributedCompanyDto> GetContributedCompanyByIdAsync(string id)
        {
            var contributedCompany = await _contributedCompanyRepository.GetByIdAsync(id);
            return _mapper.Map<ContributedCompanyDto>(contributedCompany);
        }

        public async Task<IEnumerable<ContributedCompanyDto>> GetContributedCompaniesByProjectIdAsync(string projectId)
        {
            // Verify that the project exists
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {projectId} not found.");
            }

            var contributedCompanies = await _contributedCompanyRepository.GetByProjectIdAsync(projectId);
            return _mapper.Map<IEnumerable<ContributedCompanyDto>>(contributedCompanies);
        }

        public async Task<IEnumerable<ContributedCompanyDto>> GetProjectsByCompanyIdAsync(string companyId)
        {
            // Verify that the company exists
            var company = await _companyRepository.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new KeyNotFoundException($"Company with ID {companyId} not found.");
            }

            var contributedCompanies = await _contributedCompanyRepository.GetByCompanyIdAsync(companyId);
            return _mapper.Map<IEnumerable<ContributedCompanyDto>>(contributedCompanies);
        }

        public async Task<ContributedCompanyDto> CreateContributedCompanyAsync(CreateContributedCompanyDto createContributedCompanyDto)
        {
            // Verify that the company exists
            var company = await _companyRepository.GetByIdAsync(createContributedCompanyDto.CompanyId);
            if (company == null)
            {
                throw new KeyNotFoundException($"Company with ID {createContributedCompanyDto.CompanyId} not found.");
            }

            // Verify that the project exists
            var project = await _projectRepository.GetByIdAsync(createContributedCompanyDto.ProjectId);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {createContributedCompanyDto.ProjectId} not found.");
            }

            // Check if the association already exists
            var existingAssociations = await _contributedCompanyRepository.FindAsync(
                cc => cc.CompanyId == createContributedCompanyDto.CompanyId && cc.ProjectId == createContributedCompanyDto.ProjectId);
            
            if (existingAssociations.GetEnumerator().MoveNext())
            {
                throw new InvalidOperationException($"Company with ID {createContributedCompanyDto.CompanyId} is already associated with Project with ID {createContributedCompanyDto.ProjectId}");
            }

            var contributedCompanyEntity = _mapper.Map<ContributedCompany>(createContributedCompanyDto);
            await _contributedCompanyRepository.CreateAsync(contributedCompanyEntity);
            return _mapper.Map<ContributedCompanyDto>(contributedCompanyEntity);
        }

        public async Task DeleteContributedCompanyAsync(string id)
        {
            var contributedCompany = await _contributedCompanyRepository.GetByIdAsync(id);
            if (contributedCompany == null)
            {
                throw new KeyNotFoundException($"Contributed company with ID {id} not found.");
            }

            await _contributedCompanyRepository.DeleteAsync(id);
        }
    }

    public interface IContributedCompanyService
    {
        Task<IEnumerable<ContributedCompanyDto>> GetAllContributedCompaniesAsync();
        Task<ContributedCompanyDto> GetContributedCompanyByIdAsync(string id);
        Task<IEnumerable<ContributedCompanyDto>> GetContributedCompaniesByProjectIdAsync(string projectId);
        Task<IEnumerable<ContributedCompanyDto>> GetProjectsByCompanyIdAsync(string companyId);
        Task<ContributedCompanyDto> CreateContributedCompanyAsync(CreateContributedCompanyDto createContributedCompanyDto);
        Task DeleteContributedCompanyAsync(string id);
    }
}