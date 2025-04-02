using System.ComponentModel.DataAnnotations;

namespace MongoDotNetBackend.DTOs
{
    public class SchemeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FolderId { get; set; }
        public List<string> RelatedPrograms { get; set; } = new List<string>();
        public string FilePath { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class CreateSchemeDto
    {
        [Required(ErrorMessage = "Scheme name is required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Folder ID is required")]
        public string FolderId { get; set; }
        
        public List<string> RelatedPrograms { get; set; } = new List<string>();
        public string FilePath { get; set; }
        public string Type { get; set; }
    }

    public class UpdateSchemeDto
    {
        public string Name { get; set; }
        public string FolderId { get; set; }
        public List<string> RelatedPrograms { get; set; }
        public string FilePath { get; set; }
        public string Type { get; set; }
    }
}

