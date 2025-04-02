using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Repositories;
using AutoMapper;

namespace MongoDotNetBackend.Services
{
    
    public class LetterTemplateService : ILetterTemplateService
    {
        private readonly ILetterTemplateRepository _letterTemplateRepository;
        private readonly IMapper _mapper;

        public LetterTemplateService(
            ILetterTemplateRepository letterTemplateRepository,
            IMapper mapper)
        {
            _letterTemplateRepository = letterTemplateRepository ?? throw new ArgumentNullException(nameof(letterTemplateRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<LetterTemplateDto>> GetAllLetterTemplatesAsync()
        {
            var letterTemplates = await _letterTemplateRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LetterTemplateDto>>(letterTemplates);
        }

        public async Task<LetterTemplateDto> GetLetterTemplateByIdAsync(string id)
        {
            var letterTemplate = await _letterTemplateRepository.GetByIdAsync(id);
            if (letterTemplate == null)
            {
                throw new KeyNotFoundException($"Letter template with ID {id} not found.");
            }
            
            return _mapper.Map<LetterTemplateDto>(letterTemplate);
        }

        public async Task<IEnumerable<LetterTemplateDto>> SearchLetterTemplatesByTitleAsync(string searchTerm)
        {
            var letterTemplates = await _letterTemplateRepository.SearchByTitleAsync(searchTerm);
            return _mapper.Map<IEnumerable<LetterTemplateDto>>(letterTemplates);
        }

        public async Task<LetterTemplateDto> CreateLetterTemplateAsync(CreateLetterTemplateDto createLetterTemplateDto)
        {
            var letterTemplateEntity = _mapper.Map<LetterTemplate>(createLetterTemplateDto);
            letterTemplateEntity.CreatedDate = DateTime.UtcNow;
            letterTemplateEntity.LastModifiedDate = DateTime.UtcNow;
            
            await _letterTemplateRepository.CreateAsync(letterTemplateEntity);
            return _mapper.Map<LetterTemplateDto>(letterTemplateEntity);
        }

        public async Task UpdateLetterTemplateAsync(string id, UpdateLetterTemplateDto updateLetterTemplateDto)
        {
            var letterTemplateEntity = await _letterTemplateRepository.GetByIdAsync(id);
            if (letterTemplateEntity == null)
            {
                throw new KeyNotFoundException($"Letter template with ID {id} not found.");
            }

            _mapper.Map(updateLetterTemplateDto, letterTemplateEntity);
            letterTemplateEntity.LastModifiedDate = DateTime.UtcNow;
            
            await _letterTemplateRepository.UpdateAsync(id, letterTemplateEntity);
        }

        public async Task DeleteLetterTemplateAsync(string id)
        {
            var letterTemplate = await _letterTemplateRepository.GetByIdAsync(id);
            if (letterTemplate == null)
            {
                throw new KeyNotFoundException($"Letter template with ID {id} not found.");
            }

            await _letterTemplateRepository.DeleteAsync(id);
        }
    }


    public interface ILetterTemplateService
    {
        Task<IEnumerable<LetterTemplateDto>> GetAllLetterTemplatesAsync();
        Task<LetterTemplateDto> GetLetterTemplateByIdAsync(string id);
        Task<IEnumerable<LetterTemplateDto>> SearchLetterTemplatesByTitleAsync(string searchTerm);
        Task<LetterTemplateDto> CreateLetterTemplateAsync(CreateLetterTemplateDto createLetterTemplateDto);
        Task UpdateLetterTemplateAsync(string id, UpdateLetterTemplateDto updateLetterTemplateDto);
        Task DeleteLetterTemplateAsync(string id);
    }
}
