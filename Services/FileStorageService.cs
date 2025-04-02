// Services/FileStorageService.cs
using AutoMapper;
using Microsoft.AspNetCore.Http;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MongoDotNetBackend.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IFileStorageRepository _fileStorageRepository;
        private readonly IMapper _mapper;
        private readonly string _uploadsFolder;
        private readonly string _baseUrl;

        public FileStorageService(
            IFileStorageRepository fileStorageRepository,
            IMapper mapper)
        {
            _fileStorageRepository = fileStorageRepository;
            _mapper = mapper;
            
            _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(_uploadsFolder))
            {
                Directory.CreateDirectory(_uploadsFolder);
            }
            
            _baseUrl = "http://5.189.185.195:8080/uploads";
        }

        public async Task<IEnumerable<FileStorageDto>> GetAllFilesAsync()
        {
            var files = await _fileStorageRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FileStorageDto>>(files);
        }

        public async Task<FileStorageDto> GetFileByIdAsync(string id)
        {
            var file = await _fileStorageRepository.GetByIdAsync(id);
            return _mapper.Map<FileStorageDto>(file);
        }

        public async Task<FileStorageDto> UploadFileAsync(IFormFile file)
        {
            // Generate a unique filename
            string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            string filePath = Path.Combine(_uploadsFolder, fileName);
            
            // Save file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            // Create file storage entry
            var fileStorage = new FileStorage
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                Size = file.Length,
                UploadDate = DateTime.UtcNow,
                Path = filePath,
                PublicUrl = $"{_baseUrl}/{fileName}"
            };
            
            await _fileStorageRepository.CreateAsync(fileStorage);
            
            return _mapper.Map<FileStorageDto>(fileStorage);
        }

        public async Task DeleteFileAsync(string id)
        {
            var file = await _fileStorageRepository.GetByIdAsync(id);
            if (file == null)
            {
                throw new KeyNotFoundException($"File with ID {id} not found.");
            }
            
            // Delete physical file
            if (File.Exists(file.Path))
            {
                File.Delete(file.Path);
            }
            
            // Delete database entry
            await _fileStorageRepository.DeleteAsync(id);
        }
    }

    public interface IFileStorageService
    {
        Task<IEnumerable<FileStorageDto>> GetAllFilesAsync();
        Task<FileStorageDto> GetFileByIdAsync(string id);
        Task<FileStorageDto> UploadFileAsync(IFormFile file);
        Task DeleteFileAsync(string id);
    }
}