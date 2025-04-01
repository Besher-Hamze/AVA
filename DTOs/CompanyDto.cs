using System.ComponentModel.DataAnnotations;

namespace MongoDotNetBackend.DTOs
{
 
      public class CompanyDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Thumbnail image is required")]
        public string ThumbnailImage { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        
        public string WorkTypeId { get; set; }
        
        public WorkTypeDto WorkType { get; set; }
    }

    public class CreateCompanyDto
    {
        [Required(ErrorMessage = "Thumbnail image is required")]
        public string ThumbnailImage { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        
        public string WorkTypeId { get; set; }
    }

    public class UpdateCompanyDto
    {
        [Required(ErrorMessage = "Thumbnail image is required")]
        public string ThumbnailImage { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        
        public string WorkTypeId { get; set; }
    }

}