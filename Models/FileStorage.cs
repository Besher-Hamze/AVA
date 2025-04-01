using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDotNetBackend.Models
{
    public class FileStorage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("fileName")]
        public string FileName { get; set; }

        [BsonElement("contentType")]
        public string ContentType { get; set; }

        [BsonElement("size")]
        public long Size { get; set; }

        [BsonElement("uploadDate")]
        public DateTime UploadDate { get; set; }

        [BsonElement("path")]
        public string Path { get; set; }

        [BsonElement("publicUrl")]
        public string PublicUrl { get; set; }
    }
}