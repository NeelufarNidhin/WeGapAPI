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
			CreateMap<AddEmployeeDto, Employee>().ReverseMap();
            CreateMap<UpdateEmployeeDto, Employee>().ReverseMap();
			CreateMap<UserDto,ApplicationUser>().ReverseMap();
            CreateMap<AddEmployerDto, Employer>().ReverseMap();
            CreateMap<UpdateEmployerDto, Employer>().ReverseMap();
           


        }
	}
}

