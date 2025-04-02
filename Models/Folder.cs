using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MongoDotNetBackend.Models
{
    public class Folder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("parentId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ParentId { get; set; }

        [BsonElement("path")]
        public string Path { get; set; }

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [BsonElement("lastModifiedDate")]
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
