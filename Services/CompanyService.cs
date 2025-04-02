using AutoMapper;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Repositories;

namespace MongoDotNetBackend.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IWorkCategoryRepository _workCategortRepository;
        private readonly IWorkTypeRepository _workTypeRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
        {
            var companies = await _companyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        public async Task<CompanyDto> GetCompanyByIdAsync(string id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto createCompanyDto)
        {
            var companyEntity = _mapper.Map<Company>(createCompanyDto);
            await _companyRepository.CreateAsync(companyEntity);
            return _mapper.Map<CompanyDto>(companyEntity);
        }

        public async Task UpdateCompanyAsync(string id, UpdateCompanyDto updateCompanyDto)
        {
            var companyEntity = await _companyRepository.GetByIdAsync(id);
            if (companyEntity == null)
            {
                throw new KeyNotFoundException($"Company with ID {id} not found.");
            }

            _mapper.Map(updateCompanyDto, companyEntity);
            await _companyRepository.UpdateAsync(id, companyEntity);
        }

        public async Task DeleteCompanyAsync(string id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
            {
                throw new KeyNotFoundException($"Company with ID {id} not found.");
            }

            await _companyRepository.DeleteAsync(id);
        }
    }

    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync();
        Task<CompanyDto> GetCompanyByIdAsync(string id);
        Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto createCompanyDto);
        Task UpdateCompanyAsync(string id, UpdateCompanyDto updateCompanyDto);
        Task DeleteCompanyAsync(string id);
    }
}