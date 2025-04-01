using MongoDotNetBackend.Models;
using MongoDotNetBackend.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDotNetBackend.Repositories
{
    public class WorkTypeRepository : MongoRepository<WorkType>, IWorkTypeRepository
    {
        public WorkTypeRepository(IMongoDbSettings settings) 
            : base(settings, settings.WorkTypesCollectionName)
        {
        }

    
        public async Task<IEnumerable<WorkType>> GetByWorkCategoryIdAsync(string workCategoryId)
        {
            var filter = Builders<WorkType>.Filter.Eq(wt => wt.WorkCategoryId, workCategoryId);
            return await _collection.Find(filter).ToListAsync();
        }
    }

    public interface IWorkTypeRepository : IRepository<WorkType>
    {
        Task<IEnumerable<WorkType>> GetByWorkCategoryIdAsync(string workCategoryId);
    }
}