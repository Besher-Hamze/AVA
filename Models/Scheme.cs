using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MongoDotNetBackend.Models
{
    public class Scheme
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("name")]
        public string Name { get; set; }

        [Required]
        [BsonElement("folderId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FolderId { get; set; }

        [BsonElement("relatedProgram")]
        public List<string> RelatedPrograms { get; set; } = new List<string>();

        [BsonElement("filePath")]
        public string FilePath { get; set; }

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [BsonElement("lastModifiedDate")]
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
    }
}

