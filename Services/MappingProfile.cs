using AutoMapper;
using MongoDotNetBackend.DTOs;
using MongoDotNetBackend.Models;

namespace MongoDotNetBackend.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Project mappings
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>();
            
            // Company mappings
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<CreateCompanyDto, Company>();
            CreateMap<UpdateCompanyDto, Company>();
            
            // Employee mappings
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();
            
            // ProjectStructure mappings
            CreateMap<ProjectStructure, ProjectStructureDto>().ReverseMap();
            CreateMap<CreateProjectStructureDto, ProjectStructure>();
            
            // ProjectUnit mappings
            CreateMap<ProjectUnit, ProjectUnitDto>().ReverseMap();
            CreateMap<CreateProjectUnitDto, ProjectUnit>();
            
            // WorkCategory mappings
            CreateMap<WorkCategory, WorkCategoryDto>().ReverseMap();
            CreateMap<CreateWorkCategoryDto, WorkCategory>();
            
            // WorkType mappings
            CreateMap<WorkType, WorkTypeDto>().ReverseMap();
            CreateMap<CreateWorkTypeDto, WorkType>();
            
            // ContributedCompany mappings
            CreateMap<ContributedCompany, ContributedCompanyDto>().ReverseMap();
            CreateMap<CreateContributedCompanyDto, ContributedCompany>();
            
            // File Storage mappings 
            CreateMap<FileStorage, FileStorageDto>().ReverseMap();

            // Letter Template mappings
            CreateMap<LetterTemplate, LetterTemplateDto>();
            CreateMap<CreateLetterTemplateDto, LetterTemplate>();
            CreateMap<UpdateLetterTemplateDto, LetterTemplate>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Folder, FolderDto>();
            CreateMap<CreateFolderDto, Folder>();
            CreateMap<UpdateFolderDto, Folder>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Plan, PlanDto>();
            CreateMap<CreatePlanDto, Plan>();
            CreateMap<UpdatePlanDto, Plan>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}