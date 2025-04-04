using MongoDB.Driver;
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Settings;

namespace MongoDotNetBackend.Repositories
{
    public class SchemeRepository : MongoRepository<Scheme>, ISchemeRepository
    {
        public SchemeRepository(IMongoDbSettings settings)
            : base(settings, "Schemes")
        {
        }

        public async Task<IEnumerable<Scheme>> GetSchemesByFolderIdAsync(string folderId)
        {
            var filter = Builders<Scheme>.Filter.Eq(p => p.FolderId, folderId);
            return await _collection.Find(filter).ToListAsync();
        }

    }
    public interface ISchemeRepository : IRepository<Scheme>
    {
        Task<IEnumerable<Scheme>> GetSchemesByFolderIdAsync(string folderId);
    }

}
