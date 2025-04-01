using System.ComponentModel.DataAnnotations;

namespace MongoDotNetBackend.DTOs
{
    public class EmployeeDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile phone is required")]
        [Phone(ErrorMessage = "Invalid mobile phone number format")]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "Land phone is required")]
        [Phone(ErrorMessage = "Invalid land phone number format")]
        public string LandPhone { get; set; }

        [Required(ErrorMessage = "Fax is required")]
        public string Fax { get; set; }

        [Required(ErrorMessage = "IsActive is required")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "IsReceiver is required")]
        public bool IsReceiver { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        public string CompanyId { get; set; }
        
        public CompanyDto Company { get; set; }
        
        // We don't include password in the response DTO for security reasons
    }

    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "Position is required")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile phone is required")]
        [Phone(ErrorMessage = "Invalid mobile phone number format")]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "Land phone is required")]
        [Phone(ErrorMessage = "Invalid land phone number format")]
        public string LandPhone { get; set; }

        [Required(ErrorMessage = "Fax is required")]
        public string Fax { get; set; }

        [Required(ErrorMessage = "IsActive is required")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "IsReceiver is required")]
        public bool IsReceiver { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        public string CompanyId { get; set; }
        
        // Add password field for creation
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }
    }

    public class UpdateEmployeeDto
    {
        [Required(ErrorMessage = "Position is required")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile phone is required")]
        [Phone(ErrorMessage = "Invalid mobile phone number format")]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "Land phone is required")]
        [Phone(ErrorMessage = "Invalid land phone number format")]
        public string LandPhone { get; set; }

        [Required(ErrorMessage = "Fax is required")]
        public string Fax { get; set; }

        [Required(ErrorMessage = "IsActive is required")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "IsReceiver is required")]
        public bool IsReceiver { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        public string CompanyId { get; set; }
        
        // Password is optional during updates
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }
    }
    
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Current password is required")]
        public string CurrentPassword { get; set; }
        
        [Required(ErrorMessage = "New password is required")]
        [MinLength(6, ErrorMessage = "New password must be at least 6 characters")]
        public string NewPassword { get; set; }
        
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}