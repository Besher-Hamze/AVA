using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDotNetBackend.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("short_name")]
        public string ShortName { get; set; }

        [Required]
        [BsonElement("name")]
        public string Name { get; set; }

        [Required]
        [BsonElement("owner")]
        public string Owner { get; set; }

        [Required]
        [BsonElement("phone")]
        public string Phone { get; set; }

        [Required]
        [BsonElement("email")]
        public string Email { get; set; }

        [Required]
        [BsonElement("thumbnail")]
        public string Thumbnail { get; set; }

        [Required]
        [BsonElement("address")]
        public string Address { get; set; }
    }
}