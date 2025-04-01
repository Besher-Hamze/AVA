using AutoMapper;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDotNetBackend.Services
{
    public class WorkTypeService : IWorkTypeService
    {
        private readonly IWorkTypeRepository _workTypeRepository;
        private readonly IWorkCategoryRepository _workCategoryRepository;
        private readonly IMapper _mapper;

        public WorkTypeService(
            IWorkTypeRepository workTypeRepository,
            IWorkCategoryRepository workCategoryRepository,
            IMapper mapper)
        {
            _workTypeRepository = workTypeRepository ?? throw new ArgumentNullException(nameof(workTypeRepository));
            _workCategoryRepository = workCategoryRepository ?? throw new ArgumentNullException(nameof(workCategoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<WorkTypeDto>> GetAllWorkTypesAsync()
        {
            var workTypes = await _workTypeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<WorkTypeDto>>(workTypes);
        }

        public async Task<WorkTypeDto> GetWorkTypeByIdAsync(string id)
        {
            var workType = await _workTypeRepository.GetByIdAsync(id);
            return _mapper.Map<WorkTypeDto>(workType);
        }

        public async Task<IEnumerable<WorkTypeDto>> GetWorkTypesByWorkCategoryIdAsync(string workCategoryId)
        {
            // Verify that the work category exists
            var workCategory = await _workCategoryRepository.GetByIdAsync(workCategoryId);
            if (workCategory == null)
            {
                throw new KeyNotFoundException($"Work category with ID {workCategoryId} not found.");
            }

            var workTypes = await _workTypeRepository.GetByWorkCategoryIdAsync(workCategoryId);
            return _mapper.Map<IEnumerable<WorkTypeDto>>(workTypes);
        }

        public async Task<WorkTypeDto> CreateWorkTypeAsync(CreateWorkTypeDto createWorkTypeDto)
        {
            // Verify that the work category exists
            var workCategory = await _workCategoryRepository.GetByIdAsync(createWorkTypeDto.WorkCategoryId);
            if (workCategory == null)
            {
                throw new KeyNotFoundException($"Work category with ID {createWorkTypeDto.WorkCategoryId} not found.");
            }

            var workTypeEntity = _mapper.Map<WorkType>(createWorkTypeDto);
            await _workTypeRepository.CreateAsync(workTypeEntity);
            return _mapper.Map<WorkTypeDto>(workTypeEntity);
        }

        public async Task DeleteWorkTypeAsync(string id)
        {
            var workType = await _workTypeRepository.GetByIdAsync(id);
            if (workType == null)
            {
                throw new KeyNotFoundException($"Work type with ID {id} not found.");
            }

            await _workTypeRepository.DeleteAsync(id);
        }
    }

    public interface IWorkTypeService
    {
        Task<IEnumerable<WorkTypeDto>> GetAllWorkTypesAsync();
        Task<WorkTypeDto> GetWorkTypeByIdAsync(string id);
        Task<IEnumerable<WorkTypeDto>> GetWorkTypesByWorkCategoryIdAsync(string workCategoryId);
        Task<WorkTypeDto> CreateWorkTypeAsync(CreateWorkTypeDto createWorkTypeDto);
        Task DeleteWorkTypeAsync(string id);
    }
}