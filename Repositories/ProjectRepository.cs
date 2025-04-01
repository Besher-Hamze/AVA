using MongoDotNetBackend.Models;
using MongoDotNetBackend.Settings;

namespace MongoDotNetBackend.Repositories
{
    public class ProjectRepository : MongoRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IMongoDbSettings settings) 
            : base(settings, settings.ProjectsCollectionName)
        {
        }
    }

    public interface IProjectRepository : IRepository<Project>
    {
    }
}