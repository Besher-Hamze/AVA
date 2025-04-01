using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDotNetBackend.Models
{
    public class ProjectUnit
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("excel_file")]
        public string ExcelFile { get; set; }

        [Required]
        [BsonElement("project")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProjectId { get; set; }
    }
}