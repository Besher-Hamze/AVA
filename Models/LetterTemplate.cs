using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MongoDotNetBackend.Models
{
    public class LetterTemplate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("recipient")]
        public string Recipient { get; set; }

        [BsonElement("subject")]
        public string Subject { get; set; }

        [BsonElement("introduction")]
        public string Introduction { get; set; }

        [BsonElement("body")]
        public string Body { get; set; }

        [BsonElement("conclusion")]
        public string Conclusion { get; set; }

        [BsonElement("farewell")]
        public string Farewell { get; set; }

        [BsonElement("thumbnail_image")]
        public string ThumbnailImage { get; set; }

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [BsonElement("lastModifiedDate")]
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
