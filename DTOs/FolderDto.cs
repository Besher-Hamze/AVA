using System.ComponentModel.DataAnnotations;

namespace MongoDotNetBackend.DTOs
{
    public class FolderDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string Path { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public List<FolderDto> SubFolders { get; set; } = new List<FolderDto>();
        public List<PlanDto> Plans { get; set; } = new List<PlanDto>();
    }

    public class CreateFolderDto
    {
        [Required(ErrorMessage = "Folder name is required")]
        public string Name { get; set; }
        public string ParentId { get; set; }
    }

    public class UpdateFolderDto
    {
        public string Name { get; set; }
        public string ParentId { get; set; }
    }
}

