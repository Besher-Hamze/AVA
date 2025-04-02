using MongoDB.Driver;
using MongoDotNetBackend.Models;
using MongoDotNetBackend.Settings;

namespace MongoDotNetBackend.Repositories
{
    public class LetterTemplateRepository : MongoRepository<LetterTemplate>, ILetterTemplateRepository
    {
        public LetterTemplateRepository(IMongoDbSettings settings)
            : base(settings, "LetterTemplates")
        {
        }

        public async Task<IEnumerable<LetterTemplate>> SearchByTitleAsync(string searchTerm)
        {
            var filter = Builders<LetterTemplate>.Filter.Regex(lt => lt.Title, 
                new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"));
            return await _collection.Find(filter).ToListAsync();
        }
    }
}



namespace MongoDotNetBackend.Repositories
{
    public interface ILetterTemplateRepository : IRepository<LetterTemplate>
    {
        Task<IEnumerable<LetterTemplate>> SearchByTitleAsync(string searchTerm);
    }
}
