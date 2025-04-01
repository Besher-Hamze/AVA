using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDotNetBackend.Models
{
    public class Company
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("thumbnail_image")]
        public string ThumbnailImage { get; set; }

        [Required]
        [BsonElement("name")]
        public string Name { get; set; }

        [Required]
        [BsonElement("phone")]
        public string Phone { get; set; }

        [Required]
        [BsonElement("email")]
        public string Email { get; set; }

        [Required]
        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("work_type_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string WorkTypeId { get; set; }
    }
}