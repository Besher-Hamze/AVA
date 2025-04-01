using MongoDotNetBackend.Models;
using MongoDotNetBackend.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDotNetBackend.Repositories
{
    public class ContributedCompanyRepository : MongoRepository<ContributedCompany>, IContributedCompanyRepository
    {
        public ContributedCompanyRepository(IMongoDbSettings settings) 
            : base(settings, settings.ContributedCompaniesCollectionName)
        {
        }

        public async Task<IEnumerable<ContributedCompany>> GetByProjectIdAsync(string projectId)
        {
            var filter = Builders<ContributedCompany>.Filter.Eq(cc => cc.ProjectId, projectId);
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<ContributedCompany>> GetByCompanyIdAsync(string companyId)
        {
            var filter = Builders<ContributedCompany>.Filter.Eq(cc => cc.CompanyId, companyId);
            return await _collection.Find(filter).ToListAsync();
        }
    }

    public interface IContributedCompanyRepository : IRepository<ContributedCompany>
    {
        Task<IEnumerable<ContributedCompany>> GetByProjectIdAsync(string projectId);
        Task<IEnumerable<ContributedCompany>> GetByCompanyIdAsync(string companyId);
    }
}