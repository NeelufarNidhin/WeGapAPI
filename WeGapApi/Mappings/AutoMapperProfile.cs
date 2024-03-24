using System;
using AutoMapper;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

namespace WeGapApi.Mappings
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Employer, EmployerDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
			CreateMap<Job, JobDto>().ReverseMap();
			CreateMap<Experience, ExperienceDto>().ReverseMap();
			CreateMap<AddEmployeeDto, Employee>().ReverseMap();
            CreateMap<UpdateEmployeeDto, Employee>().ReverseMap();
			CreateMap<UserDto,ApplicationUser>().ReverseMap();
            CreateMap<AddEmployerDto, Employer>().ReverseMap();
            CreateMap<UpdateEmployerDto, Employer>().ReverseMap();
			CreateMap<AddJobDto, Job>().ReverseMap();
			CreateMap<UpdateJobDto, Job>().ReverseMap();
			CreateMap<AddExperienceDto, Experience>().ReverseMap();
			CreateMap<UpdateExperienceDto, Experience>().ReverseMap();
			CreateMap<Education, EducationDto>().ReverseMap();
			CreateMap<AddEducationDto, Education>().ReverseMap();
			CreateMap<UpdateEducationDto, Education>().ReverseMap();
			CreateMap<JobSkill, JobSkillDto>().ReverseMap();
			CreateMap<AddJobSkillDto, JobSkill>().ReverseMap();
			CreateMap<UpdateJobSkillDto, JobSkill>().ReverseMap();
			CreateMap<JobType, JobTypeDto>().ReverseMap();
			CreateMap<AddJobTypeDto, JobType>().ReverseMap();
			CreateMap<UpdateJobTypeDto, JobType>().ReverseMap();
			CreateMap<Skill, SkillDto>().ReverseMap();
			CreateMap<AddSkillDto, Skill>().ReverseMap();
			CreateMap<UpdateSkillDto, Skill>().ReverseMap();
        }
	}
}

