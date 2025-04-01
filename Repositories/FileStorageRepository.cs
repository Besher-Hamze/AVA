// Repositories/FileStorageRepository.cs
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDotNetBackend.Repositories
{
    public class FileStorageRepository : MongoRepository<FileStorage>, IFileStorageRepository
    {
        public FileStorageRepository(IMongoDbSettings settings) 
            : base(settings, "FileStorage")
        {
        }
    }

    public interface IFileStorageRepository : IRepository<FileStorage>
    {
    }
}