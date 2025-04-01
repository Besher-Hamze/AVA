namespace MongoDotNetBackend.Settings
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ProjectsCollectionName { get; set; }
        public string CompaniesCollectionName { get; set; }
        public string EmployeesCollectionName { get; set; }
        public string ProjectStructuresCollectionName { get; set; }
        public string ProjectUnitsCollectionName { get; set; }
        public string WorkCategoriesCollectionName { get; set; }
        public string WorkTypesCollectionName { get; set; }
        public string ContributedCompaniesCollectionName { get; set; }
    }

    public interface IMongoDbSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ProjectsCollectionName { get; set; }
        string CompaniesCollectionName { get; set; }
        string EmployeesCollectionName { get; set; }
        string ProjectStructuresCollectionName { get; set; }
        string ProjectUnitsCollectionName { get; set; }
        string WorkCategoriesCollectionName { get; set; }
        string WorkTypesCollectionName { get; set; }
        string ContributedCompaniesCollectionName { get; set; }
    }
}