using MongoDB.Driver;
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Settings;

namespace MongoDotNetBackend.Repositories
{
    public class FolderRepository : MongoRepository<Folder>, IFolderRepository
    {
        public FolderRepository(IMongoDbSettings settings)
            : base(settings, "Folders")
        {
        }

        public async Task<IEnumerable<Folder>> GetSubFoldersAsync(string parentId)
        {
            var filter = Builders<Folder>.Filter.Eq(f => f.ParentId, parentId);
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Folder>> GetRootFoldersAsync()
        {
            var filter = Builders<Folder>.Filter.Eq(f => f.ParentId, null);
            return await _collection.Find(filter).ToListAsync();
        }
    }

    public interface IFolderRepository : IRepository<Folder>
    {
        Task<IEnumerable<Folder>> GetSubFoldersAsync(string parentId);
        Task<IEnumerable<Folder>> GetRootFoldersAsync();
    }

}
