using MongoDotNetBackend.Models;
using MongoDotNetBackend.Settings;

namespace MongoDotNetBackend.Repositories
{
    public class CompanyRepository : MongoRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IMongoDbSettings settings) 
            : base(settings, settings.CompaniesCollectionName)
        {
        }
    }

    public interface ICompanyRepository : IRepository<Company>
    {
    }
}