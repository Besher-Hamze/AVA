using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDotNetBackend.Models
{
    public class WorkType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("type")]
        public string Type { get; set; }

        [Required]
        [BsonElement("work_category")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string WorkCategoryId { get; set; }
    }
}