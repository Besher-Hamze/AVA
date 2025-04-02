using AutoMapper;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Repositories;

namespace MongoDotNetBackend.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;

        public PlanService(
            IPlanRepository planRepository,
            IFolderRepository folderRepository,
            IMapper mapper)
        {
            _planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            _folderRepository = folderRepository ?? throw new ArgumentNullException(nameof(folderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<PlanDto>> GetAllPlansAsync()
        {
            var plans = await _planRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PlanDto>>(plans);
        }

        public async Task<PlanDto> GetPlanByIdAsync(string id)
        {
            var plan = await _planRepository.GetByIdAsync(id);
            if (plan == null)
            {
                throw new KeyNotFoundException($"Plan with ID {id} not found.");
            }
            
            return _mapper.Map<PlanDto>(plan);
        }

        public async Task<IEnumerable<PlanDto>> GetPlansByFolderIdAsync(string folderId)
        {
            var folder = await _folderRepository.GetByIdAsync(folderId);
            if (folder == null)
            {
                throw new KeyNotFoundException($"Folder with ID {folderId} not found.");
            }

            var plans = await _planRepository.GetPlansByFolderIdAsync(folderId);
            return _mapper.Map<IEnumerable<PlanDto>>(plans);
        }

        public async Task<IEnumerable<PlanDto>> GetPlansByTypeAsync(string type)
        {
            var plans = await _planRepository.GetPlansByTypeAsync(type);
            return _mapper.Map<IEnumerable<PlanDto>>(plans);
        }

        public async Task<PlanDto> CreatePlanAsync(CreatePlanDto createPlanDto)
        {
            // Validate folder
            var folder = await _folderRepository.GetByIdAsync(createPlanDto.FolderId);
            if (folder == null)
            {
                throw new KeyNotFoundException($"Folder with ID {createPlanDto.FolderId} not found.");
            }

            var planEntity = _mapper.Map<Plan>(createPlanDto);
            planEntity.CreatedDate = DateTime.UtcNow;
            planEntity.LastModifiedDate = DateTime.UtcNow;
            
            await _planRepository.CreateAsync(planEntity);
            return _mapper.Map<PlanDto>(planEntity);
        }

        public async Task UpdatePlanAsync(string id, UpdatePlanDto updatePlanDto)
        {
            var planEntity = await _planRepository.GetByIdAsync(id);
            if (planEntity == null)
            {
                throw new KeyNotFoundException($"Plan with ID {id} not found.");
            }

            if (!string.IsNullOrEmpty(updatePlanDto.FolderId) && updatePlanDto.FolderId != planEntity.FolderId)
            {
                var folder = await _folderRepository.GetByIdAsync(updatePlanDto.FolderId);
                if (folder == null)
                {
                    throw new KeyNotFoundException($"Folder with ID {updatePlanDto.FolderId} not found.");
                }
            }

            _mapper.Map(updatePlanDto, planEntity);
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

    public interface IPlanService
    {
        Task<IEnumerable<PlanDto>> GetAllPlansAsync();
        Task<PlanDto> GetPlanByIdAsync(string id);
        Task<IEnumerable<PlanDto>> GetPlansByFolderIdAsync(string folderId);
        Task<IEnumerable<PlanDto>> GetPlansByTypeAsync(string type);
        Task<PlanDto> CreatePlanAsync(CreatePlanDto createPlanDto);
        Task UpdatePlanAsync(string id, UpdatePlanDto updatePlanDto);
        Task DeletePlanAsync(string id);
    }
}
