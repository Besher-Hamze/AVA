using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDotNetBackend.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("position")]
        public string Position { get; set; }

        [Required]
        [BsonElement("gender")]
        public string Gender { get; set; }

        [Required]
        [BsonElement("first_name")]
        public string FirstName { get; set; }

        [Required]
        [BsonElement("last_name")]
        public string LastName { get; set; }

        [Required]
        [BsonElement("email")]
        public string Email { get; set; }

        [Required]
        [BsonElement("mobile_phone")]
        public string MobilePhone { get; set; }

        [Required]
        [BsonElement("land_phone")]
        public string LandPhone { get; set; }

        [Required]
        [BsonElement("fax")]
        public string Fax { get; set; }

        [Required]
        [BsonElement("is_active")]
        public bool IsActive { get; set; }

        [Required]
        [BsonElement("is_receiver")]
        public bool IsReceiver { get; set; }

        [Required]
        [BsonElement("company_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CompanyId { get; set; }
        
        [Required]
        [BsonElement("password_hash")]
        public string PasswordHash { get; set; }
        
        [BsonIgnore]
        public Company Company { get; set; }
    }
}