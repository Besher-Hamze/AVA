using MongoDotNetBackend.Models;
using MongoDotNetBackend.Settings;
using MongoDB.Driver;

namespace MongoDotNetBackend.Repositories
{
    public class EmployeeRepository : MongoRepository<Employee>, IEmployeeRepository
    {
        private readonly IMongoCollection<Company> _companyCollection;
        
        public EmployeeRepository(IMongoDbSettings settings) 
            : base(settings, settings.EmployeesCollectionName)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _companyCollection = database.GetCollection<Company>(settings.CompaniesCollectionName);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByCompanyIdAsync(string companyId)
        {
            var filter = Builders<Employee>.Filter.Eq(e => e.CompanyId, companyId);
            return await _collection.Find(filter).ToListAsync();
        }
        
        public override async Task<Employee> GetByIdAsync(string id)
        {
            var filter = Builders<Employee>.Filter.Eq("_id", MongoDB.Bson.ObjectId.Parse(id));
            var employee = await _collection.Find(filter).FirstOrDefaultAsync();
            
            if (employee != null && !string.IsNullOrEmpty(employee.CompanyId))
            {
                var companyFilter = Builders<Company>.Filter.Eq("_id", MongoDB.Bson.ObjectId.Parse(employee.CompanyId));
                var company = await _companyCollection.Find(companyFilter).FirstOrDefaultAsync();
                employee.Company = company;
            }
            
            return employee;
        }
        public async Task<Employee> GetEmployeeByEmailAsync(string email)
{
    var filter = Builders<Employee>.Filter.Eq(e => e.Email, email);
    return await _collection.Find(filter).FirstOrDefaultAsync();
}

    }

    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesByCompanyIdAsync(string companyId);
        Task<Employee> GetEmployeeByEmailAsync(string email);

    }
}