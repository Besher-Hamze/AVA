using System.ComponentModel.DataAnnotations;

namespace MongoDotNetBackend.DTOs
{
    public class ProjectDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Short name is required")]
        public string ShortName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Owner is required")]
        public string Owner { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Thumbnail is required")]
        public string Thumbnail { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }

    public class CreateProjectDto
    {
        [Required(ErrorMessage = "Short name is required")]
        public string ShortName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Owner is required")]
        public string Owner { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Thumbnail is required")]
        public string Thumbnail { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }

    public class UpdateProjectDto
    {
        [Required(ErrorMessage = "Short name is required")]
        public string ShortName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Owner is required")]
        public string Owner { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Thumbnail is required")]
        public string Thumbnail { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }
}