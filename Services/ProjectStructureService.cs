using AutoMapper;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDotNetBackend.Services
{
    public class ProjectStructureService : IProjectStructureService
    {
        private readonly IProjectStructureRepository _projectStructureRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectStructureService(
            IProjectStructureRepository projectStructureRepository,
            IProjectRepository projectRepository,
            IMapper mapper)
        {
            _projectStructureRepository = projectStructureRepository ?? throw new ArgumentNullException(nameof(projectStructureRepository));
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ProjectStructureDto>> GetAllProjectStructuresAsync()
        {
            var projectStructures = await _projectStructureRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectStructureDto>>(projectStructures);
        }

        public async Task<ProjectStructureDto> GetProjectStructureByIdAsync(string id)
        {
            var projectStructure = await _projectStructureRepository.GetByIdAsync(id);
            return _mapper.Map<ProjectStructureDto>(projectStructure);
        }

        public async Task<IEnumerable<ProjectStructureDto>> GetProjectStructuresByProjectIdAsync(string projectId)
        {
            // Verify that the project exists
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {projectId} not found.");
            }

            var projectStructures = await _projectStructureRepository.GetByProjectIdAsync(projectId);
            return _mapper.Map<IEnumerable<ProjectStructureDto>>(projectStructures);
        }

        public async Task<ProjectStructureDto> CreateProjectStructureAsync(CreateProjectStructureDto createProjectStructureDto)
        {
            // Verify that the project exists
            var project = await _projectRepository.GetByIdAsync(createProjectStructureDto.ProjectId);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {createProjectStructureDto.ProjectId} not found.");
            }

            var projectStructureEntity = _mapper.Map<ProjectStructure>(createProjectStructureDto);
            await _projectStructureRepository.CreateAsync(projectStructureEntity);
            return _mapper.Map<ProjectStructureDto>(projectStructureEntity);
        }

        public async Task DeleteProjectStructureAsync(string id)
        {
            var projectStructure = await _projectStructureRepository.GetByIdAsync(id);
            if (projectStructure == null)
            {
                throw new KeyNotFoundException($"Project structure with ID {id} not found.");
            }

            await _projectStructureRepository.DeleteAsync(id);
        }
    }

    public interface IProjectStructureService
    {
        Task<IEnumerable<ProjectStructureDto>> GetAllProjectStructuresAsync();
        Task<ProjectStructureDto> GetProjectStructureByIdAsync(string id);
        Task<IEnumerable<ProjectStructureDto>> GetProjectStructuresByProjectIdAsync(string projectId);
        Task<ProjectStructureDto> CreateProjectStructureAsync(CreateProjectStructureDto createProjectStructureDto);
        Task DeleteProjectStructureAsync(string id);
    }
}
