using AutoMapper;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Repositories;

namespace MongoDotNetBackend.Services
{
    public class FolderService : IFolderService
    {
        private readonly IFolderRepository _folderRepository;
        private readonly ISchemeRepository _schemeRepository;
        private readonly IMapper _mapper;

        public FolderService(
            IFolderRepository folderRepository,
            ISchemeRepository schemeRepository,
            IMapper mapper)
        {
            _folderRepository = folderRepository ?? throw new ArgumentNullException(nameof(folderRepository));
            _schemeRepository = schemeRepository ?? throw new ArgumentNullException(nameof(schemeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<FolderDto>> GetAllFoldersAsync()
        {
            var folders = await _folderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FolderDto>>(folders);
        }

        public async Task<FolderDto> GetFolderByIdAsync(string id, bool includeChildren = false)
        {
            var folder = await _folderRepository.GetByIdAsync(id);
            if (folder == null)
            {
                throw new KeyNotFoundException($"Folder with ID {id} not found.");
            }

            var folderDto = _mapper.Map<FolderDto>(folder);

            if (includeChildren)
            {
                var subFolders = await _folderRepository.GetSubFoldersAsync(id);
                folderDto.SubFolders = _mapper.Map<List<FolderDto>>(subFolders);

                var schemes = await _schemeRepository.GetSchemesByFolderIdAsync(id);
                folderDto.schemes = _mapper.Map<List<SchemeDto>>(schemes);
            }

            return folderDto;
        }

        public async Task<IEnumerable<FolderDto>> GetRootFoldersAsync()
        {
            var folders = await _folderRepository.GetRootFoldersAsync();
            return _mapper.Map<IEnumerable<FolderDto>>(folders);
        }

        public async Task<IEnumerable<FolderDto>> GetSubFoldersAsync(string parentId)
        {
            var subFolders = await _folderRepository.GetSubFoldersAsync(parentId);
            return _mapper.Map<IEnumerable<FolderDto>>(subFolders);
        }

        public async Task<FolderDto> CreateFolderAsync(CreateFolderDto createFolderDto)
        {
            if (!string.IsNullOrEmpty(createFolderDto.ParentId))
            {
                var parentFolder = await _folderRepository.GetByIdAsync(createFolderDto.ParentId);
                if (parentFolder == null)
                {
                    throw new KeyNotFoundException($"Parent folder with ID {createFolderDto.ParentId} not found.");
                }
            }

            var folderEntity = _mapper.Map<Folder>(createFolderDto);
            folderEntity.CreatedDate = DateTime.UtcNow;
            folderEntity.LastModifiedDate = DateTime.UtcNow;

            if (!string.IsNullOrEmpty(createFolderDto.ParentId))
            {
                var parentFolder = await _folderRepository.GetByIdAsync(createFolderDto.ParentId);
                folderEntity.Path = string.IsNullOrEmpty(parentFolder.Path) 
                    ? parentFolder.Name 
                    : $"{parentFolder.Path}/{parentFolder.Name}";
            }
            else
            {
                folderEntity.Path = "";
            }

            await _folderRepository.CreateAsync(folderEntity);
            return _mapper.Map<FolderDto>(folderEntity);
        }

        public async Task UpdateFolderAsync(string id, UpdateFolderDto updateFolderDto)
        {
            var folderEntity = await _folderRepository.GetByIdAsync(id);
            if (folderEntity == null)
            {
                throw new KeyNotFoundException($"Folder with ID {id} not found.");
            }

            // Validate parent folder if specified and changed
            if (!string.IsNullOrEmpty(updateFolderDto.ParentId) && updateFolderDto.ParentId != folderEntity.ParentId)
            {
                var parentFolder = await _folderRepository.GetByIdAsync(updateFolderDto.ParentId);
                if (parentFolder == null)
                {
                    throw new KeyNotFoundException($"Parent folder with ID {updateFolderDto.ParentId} not found.");
                }

                // Update path if parent changed
                folderEntity.Path = string.IsNullOrEmpty(parentFolder.Path) 
                    ? parentFolder.Name 
                    : $"{parentFolder.Path}/{parentFolder.Name}";
            }

            _mapper.Map(updateFolderDto, folderEntity);
            folderEntity.LastModifiedDate = DateTime.UtcNow;

            await _folderRepository.UpdateAsync(id, folderEntity);
        }

        public async Task DeleteFolderAsync(string id)
        {
            var folder = await _folderRepository.GetByIdAsync(id);
            if (folder == null)
            {
                throw new KeyNotFoundException($"Folder with ID {id} not found.");
            }

            var subFolders = await _folderRepository.GetSubFoldersAsync(id);
            foreach (var subFolder in subFolders)
            {
                await DeleteFolderAsync(subFolder.Id);
            }

            var Schemes = await _schemeRepository.GetSchemesByFolderIdAsync(id);
            foreach (var Scheme in Schemes)
            {
                await _schemeRepository.DeleteAsync(Scheme.Id);
            }

            await _folderRepository.DeleteAsync(id);
        }

        public async Task<FolderDto> GetFolderWithContentsAsync(string id)
        {
            return await GetFolderByIdAsync(id, true);
        }
    }
    public interface IFolderService
    {
        Task<IEnumerable<FolderDto>> GetAllFoldersAsync();
        Task<FolderDto> GetFolderByIdAsync(string id, bool includeChildren = false);
        Task<IEnumerable<FolderDto>> GetRootFoldersAsync();
        Task<IEnumerable<FolderDto>> GetSubFoldersAsync(string parentId);
        Task<FolderDto> CreateFolderAsync(CreateFolderDto createFolderDto);
        Task UpdateFolderAsync(string id, UpdateFolderDto updateFolderDto);
        Task DeleteFolderAsync(string id);
        Task<FolderDto> GetFolderWithContentsAsync(string id);
    }

}
