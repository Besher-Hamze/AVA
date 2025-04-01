using AutoMapper;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Repositories;

namespace MongoDotNetBackend.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto> GetProjectByIdAsync(string id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto createProjectDto)
        {
            var projectEntity = _mapper.Map<Project>(createProjectDto);
            await _projectRepository.CreateAsync(projectEntity);
            return _mapper.Map<ProjectDto>(projectEntity);
        }

        public async Task UpdateProjectAsync(string id, UpdateProjectDto updateProjectDto)
        {
            var projectEntity = await _projectRepository.GetByIdAsync(id);
            if (projectEntity == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found.");
            }

            _mapper.Map(updateProjectDto, projectEntity);
            await _projectRepository.UpdateAsync(id, projectEntity);
        }

        public async Task DeleteProjectAsync(string id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found.");
            }

            await _projectRepository.DeleteAsync(id);
        }
    }

    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto> GetProjectByIdAsync(string id);
        Task<ProjectDto> CreateProjectAsync(CreateProjectDto createProjectDto);
        Task UpdateProjectAsync(string id, UpdateProjectDto updateProjectDto);
        Task DeleteProjectAsync(string id);
    }
}