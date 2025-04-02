using MongoDB.Driver;
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Settings;

namespace MongoDotNetBackend.Repositories
{
    public class PlanRepository : MongoRepository<Plan>, IPlanRepository
    {
        public PlanRepository(IMongoDbSettings settings)
            : base(settings, "Plans")
        {
        }

        public async Task<IEnumerable<Plan>> GetPlansByFolderIdAsync(string folderId)
        {
            var filter = Builders<Plan>.Filter.Eq(p => p.FolderId, folderId);
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Plan>> GetPlansByTypeAsync(string type)
        {
            var filter = Builders<Plan>.Filter.Eq(p => p.Type, type);
            return await _collection.Find(filter).ToListAsync();
        }
    }
    public interface IPlanRepository : IRepository<Plan>
    {
        Task<IEnumerable<Plan>> GetPlansByFolderIdAsync(string folderId);
        Task<IEnumerable<Plan>> GetPlansByTypeAsync(string type);
    }

}
