using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDotNetBackend.Models
{
    public class ContributedCompany
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("company")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CompanyId { get; set; }

        [Required]
        [BsonElement("project")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProjectId { get; set; }
    }
}