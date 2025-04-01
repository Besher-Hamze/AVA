using AutoMapper;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDotNetBackend.Services
{
    public class WorkCategoryService : IWorkCategoryService
    {
        private readonly IWorkCategoryRepository _workCategoryRepository;
        private readonly IMapper _mapper;

        public WorkCategoryService(
            IWorkCategoryRepository workCategoryRepository,
            IMapper mapper)
        {
            _workCategoryRepository = workCategoryRepository ?? throw new ArgumentNullException(nameof(workCategoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<WorkCategoryDto>> GetAllWorkCategoriesAsync()
        {
            var workCategories = await _workCategoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<WorkCategoryDto>>(workCategories);
        }

        public async Task<WorkCategoryDto> GetWorkCategoryByIdAsync(string id)
        {
            var workCategory = await _workCategoryRepository.GetByIdAsync(id);
            return _mapper.Map<WorkCategoryDto>(workCategory);
        }

        public async Task<WorkCategoryDto> CreateWorkCategoryAsync(CreateWorkCategoryDto createWorkCategoryDto)
        {
            var workCategoryEntity = _mapper.Map<WorkCategory>(createWorkCategoryDto);
            await _workCategoryRepository.CreateAsync(workCategoryEntity);
            return _mapper.Map<WorkCategoryDto>(workCategoryEntity);
        }

        public async Task DeleteWorkCategoryAsync(string id)
        {
            var workCategory = await _workCategoryRepository.GetByIdAsync(id);
            if (workCategory == null)
            {
                throw new KeyNotFoundException($"Work category with ID {id} not found.");
            }

            await _workCategoryRepository.DeleteAsync(id);
        }
    }

    public interface IWorkCategoryService
    {
        Task<IEnumerable<WorkCategoryDto>> GetAllWorkCategoriesAsync();
        Task<WorkCategoryDto> GetWorkCategoryByIdAsync(string id);
        Task<WorkCategoryDto> CreateWorkCategoryAsync(CreateWorkCategoryDto createWorkCategoryDto);
        Task DeleteWorkCategoryAsync(string id);
    }
}