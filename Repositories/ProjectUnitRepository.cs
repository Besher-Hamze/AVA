using MongoDotNetBackend.Models;
using MongoDotNetBackend.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDotNetBackend.Repositories
{
    public class ProjectUnitRepository : MongoRepository<ProjectUnit>, IProjectUnitRepository
    {
        public ProjectUnitRepository(IMongoDbSettings settings) 
            : base(settings, settings.ProjectUnitsCollectionName)
        {
        }

        public async Task<IEnumerable<ProjectUnit>> GetByProjectIdAsync(string projectId)
        {
            var filter = Builders<ProjectUnit>.Filter.Eq(pu => pu.ProjectId, projectId);
            return await _collection.Find(filter).ToListAsync();
        }
    }

    public interface IProjectUnitRepository : IRepository<ProjectUnit>
    {
        Task<IEnumerable<ProjectUnit>> GetByProjectIdAsync(string projectId);
    }
}