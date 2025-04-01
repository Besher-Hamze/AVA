using MongoDotNetBackend.Models;
using MongoDotNetBackend.Settings;

namespace MongoDotNetBackend.Repositories
{
    public class WorkCategoryRepository : MongoRepository<WorkCategory>, IWorkCategoryRepository
    {
        public WorkCategoryRepository(IMongoDbSettings settings) 
            : base(settings, settings.WorkCategoriesCollectionName)
        {
        }
    }

    public interface IWorkCategoryRepository : IRepository<WorkCategory>
    {
    }
}