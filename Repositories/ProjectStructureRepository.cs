using MongoDotNetBackend.Models;
using MongoDotNetBackend.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDotNetBackend.Repositories
{
    public class ProjectStructureRepository : MongoRepository<ProjectStructure>, IProjectStructureRepository
    {
        public ProjectStructureRepository(IMongoDbSettings settings) 
            : base(settings, settings.ProjectStructuresCollectionName)
        {
        }

        public async Task<IEnumerable<ProjectStructure>> GetByProjectIdAsync(string projectId)
        {
            var filter = Builders<ProjectStructure>.Filter.Eq(ps => ps.ProjectId, projectId);
            return await _collection.Find(filter).ToListAsync();
        }
    }

    public interface IProjectStructureRepository : IRepository<ProjectStructure>
    {
        Task<IEnumerable<ProjectStructure>> GetByProjectIdAsync(string projectId);
    }
}