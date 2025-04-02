using System.ComponentModel.DataAnnotations;

namespace MongoDotNetBackend.DTOs
{
    public class LetterTemplateDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Introduction { get; set; }
        public string Body { get; set; }
        public string Conclusion { get; set; }
        public string Farewell { get; set; }
        public string ThumbnailImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class CreateLetterTemplateDto
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Introduction { get; set; }
        public string Body { get; set; }
        public string Conclusion { get; set; }
        public string Farewell { get; set; }
        public string ThumbnailImage { get; set; }
    }

    public class UpdateLetterTemplateDto
    {
        public string Title { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Introduction { get; set; }
        public string Body { get; set; }
        public string Conclusion { get; set; }
        public string Farewell { get; set; }
        public string ThumbnailImage { get; set; }
    }
}

