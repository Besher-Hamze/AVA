using AutoMapper;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Repositories;

namespace MongoDotNetBackend.Services
{
    public class SchemeService : ISchemeService
    {
        private readonly ISchemeRepository _planRepository;
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;

        public SchemeService(
            ISchemeRepository planRepository,
            IFolderRepository folderRepository,
            IMapper mapper)
        {
            _planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            _folderRepository = folderRepository ?? throw new ArgumentNullException(nameof(folderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<SchemeDto>> GetAllPlansAsync()
        {
            var plans = await _planRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SchemeDto>>(plans);
        }

        public async Task<SchemeDto> GetPlanByIdAsync(string id)
        {
            var plan = await _planRepository.GetByIdAsync(id);
            if (plan == null)
            {
                throw new KeyNotFoundException($"Plan with ID {id} not found.");
            }
            
            return _mapper.Map<SchemeDto>(plan);
        }

        public async Task<IEnumerable<SchemeDto>> GetPlansByFolderIdAsync(string folderId)
        {
            var folder = await _folderRepository.GetByIdAsync(folderId);
            if (folder == null)
            {
                throw new KeyNotFoundException($"Folder with ID {folderId} not found.");
            }

            var plans = await _planRepository.GetSchemesByFolderIdAsync(folderId);
            return _mapper.Map<IEnumerable<SchemeDto>>(plans);
        }

        public async Task<IEnumerable<SchemeDto>> GetPlansByTypeAsync(string type)
        {
            var plans = await _planRepository.GetSchemesByTypeAsync(type);
            return _mapper.Map<IEnumerable<SchemeDto>>(plans);
        }

        public async Task<SchemeDto> CreatePlanAsync(CreateSchemeDto createSchemeDto)
        {
            // Validate folder
            var folder = await _folderRepository.GetByIdAsync(createSchemeDto.FolderId);
            if (folder == null)
            {
                throw new KeyNotFoundException($"Folder with ID {createSchemeDto.FolderId} not found.");
            }

            var planEntity = _mapper.Map<Scheme>(createSchemeDto);
            planEntity.CreatedDate = DateTime.UtcNow;
            planEntity.LastModifiedDate = DateTime.UtcNow;
            
            await _planRepository.CreateAsync(planEntity);
            return _mapper.Map<SchemeDto>(planEntity);
        }

        public async Task UpdatePlanAsync(string id, UpdateSchemeDto updateSchemeDto)
        {
            var planEntity = await _planRepository.GetByIdAsync(id);
            if (planEntity == null)
            {
                throw new KeyNotFoundException($"Plan with ID {id} not found.");
            }

            if (!string.IsNullOrEmpty(updateSchemeDto.FolderId) && updateSchemeDto.FolderId != planEntity.FolderId)
            {
                var folder = await _folderRepository.GetByIdAsync(updateSchemeDto.FolderId);
                if (folder == null)
                {
                    throw new KeyNotFoundException($"Folder with ID {updateSchemeDto.FolderId} not found.");
                }
            }

            _mapper.Map(updateSchemeDto, planEntity);
            planEntity.LastModifiedDate = DateTime.UtcNow;
            
            await _planRepository.UpdateAsync(id, planEntity);
        }

        public async Task DeletePlanAsync(string id)
        {
            var plan = await _planRepository.GetByIdAsync(id);
            if (plan == null)
            {
                throw new KeyNotFoundException($"Plan with ID {id} not found.");
            }

            await _planRepository.DeleteAsync(id);
        }
    }

    public interface ISchemeService
    {
        Task<IEnumerable<SchemeDto>> GetAllPlansAsync();
        Task<SchemeDto> GetPlanByIdAsync(string id);
        Task<IEnumerable<SchemeDto>> GetPlansByFolderIdAsync(string folderId);
        Task<IEnumerable<SchemeDto>> GetPlansByTypeAsync(string type);
        Task<SchemeDto> CreatePlanAsync(CreateSchemeDto createSchemeDto);
        Task UpdatePlanAsync(string id, UpdateSchemeDto updateSchemeDto);
        Task DeletePlanAsync(string id);
    }
}
