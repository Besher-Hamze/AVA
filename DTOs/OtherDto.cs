
using System.ComponentModel.DataAnnotations;

namespace MongoDotNetBackend.DTOs
{
    public class ProjectStructureDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Excel file is required")]
        public string ExcelFile { get; set; }

        [Required(ErrorMessage = "Project ID is required")]
        public string ProjectId { get; set; }
        
        public ProjectDto Project { get; set; }
    }

    public class CreateProjectStructureDto
    {
        [Required(ErrorMessage = "Excel file is required")]
        public string ExcelFile { get; set; }

        [Required(ErrorMessage = "Project ID is required")]
        public string ProjectId { get; set; }
    }

    public class ProjectUnitDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Excel file is required")]
        public string ExcelFile { get; set; }

        [Required(ErrorMessage = "Project ID is required")]
        public string ProjectId { get; set; }
        
        public ProjectDto Project { get; set; }
    }

    public class CreateProjectUnitDto
    {
        [Required(ErrorMessage = "Excel file is required")]
        public string ExcelFile { get; set; }

        [Required(ErrorMessage = "Project ID is required")]
        public string ProjectId { get; set; }
    }

    public class WorkCategoryDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }
    }

    public class CreateWorkCategoryDto
    {
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }
    }

    public class WorkTypeDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Work category ID is required")]
        public string WorkCategoryId { get; set; }
        
        public WorkCategoryDto WorkCategory { get; set; }
    }

    public class CreateWorkTypeDto
    {
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Work category ID is required")]
        public string WorkCategoryId { get; set; }
    }

    public class ContributedCompanyDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        public string CompanyId { get; set; }
        
        public CompanyDto Company { get; set; }

        [Required(ErrorMessage = "Project ID is required")]
        public string ProjectId { get; set; }
        
        public ProjectDto Project { get; set; }
    }

    public class CreateContributedCompanyDto
    {
        [Required(ErrorMessage = "Company ID is required")]
        public string CompanyId { get; set; }

        [Required(ErrorMessage = "Project ID is required")]
        public string ProjectId { get; set; }
    }
}