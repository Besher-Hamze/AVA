using AutoMapper;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDotNetBackend.Services
{
    public class ProjectUnitService : IProjectUnitService
    {
        private readonly IProjectUnitRepository _projectUnitRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectUnitService(
            IProjectUnitRepository projectUnitRepository,
            IProjectRepository projectRepository,
            IMapper mapper)
        {
            _projectUnitRepository = projectUnitRepository ?? throw new ArgumentNullException(nameof(projectUnitRepository));
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ProjectUnitDto>> GetAllProjectUnitsAsync()
        {
            var projectUnits = await _projectUnitRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectUnitDto>>(projectUnits);
        }

        public async Task<ProjectUnitDto> GetProjectUnitByIdAsync(string id)
        {
            var projectUnit = await _projectUnitRepository.GetByIdAsync(id);
            return _mapper.Map<ProjectUnitDto>(projectUnit);
        }

        public async Task<IEnumerable<ProjectUnitDto>> GetProjectUnitsByProjectIdAsync(string projectId)
        {
            // Verify that the project exists
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {projectId} not found.");
            }

            var projectUnits = await _projectUnitRepository.GetByProjectIdAsync(projectId);
            return _mapper.Map<IEnumerable<ProjectUnitDto>>(projectUnits);
        }

        public async Task<ProjectUnitDto> CreateProjectUnitAsync(CreateProjectUnitDto createProjectUnitDto)
        {
            // Verify that the project exists
            var project = await _projectRepository.GetByIdAsync(createProjectUnitDto.ProjectId);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {createProjectUnitDto.ProjectId} not found.");
            }

            var projectUnitEntity = _mapper.Map<ProjectUnit>(createProjectUnitDto);
            await _projectUnitRepository.CreateAsync(projectUnitEntity);
            return _mapper.Map<ProjectUnitDto>(projectUnitEntity);
        }

        public async Task DeleteProjectUnitAsync(string id)
        {
            var projectUnit = await _projectUnitRepository.GetByIdAsync(id);
            if (projectUnit == null)
            {
                throw new KeyNotFoundException($"Project unit with ID {id} not found.");
            }

            await _projectUnitRepository.DeleteAsync(id);
        }
    }

    public interface IProjectUnitService
    {
        Task<IEnumerable<ProjectUnitDto>> GetAllProjectUnitsAsync();
        Task<ProjectUnitDto> GetProjectUnitByIdAsync(string id);
        Task<IEnumerable<ProjectUnitDto>> GetProjectUnitsByProjectIdAsync(string projectId);
        Task<ProjectUnitDto> CreateProjectUnitAsync(CreateProjectUnitDto createProjectUnitDto);
        Task DeleteProjectUnitAsync(string id);
    }
}