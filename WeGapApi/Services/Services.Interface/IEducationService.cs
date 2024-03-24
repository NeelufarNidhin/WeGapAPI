using System;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

namespace WeGapApi.Services.Services.Interface
{
	public interface IEducationService
	{
        Task<List<EducationDto>> GetAllAsync();
        Task<EducationDto> GetEducationByIdAsync(Guid id);
        Task<EducationDto> AddEducationAsync(AddEducationDto educationDto);
        Task<EducationDto?> UpdateEducationAsync(Guid id, UpdateEducationDto educationDto);
        Task<EducationDto> DeleteEducationAsync(Guid id);
        Task<List<EducationDto>> GetEmployeeEducation(Guid id);
    }
}

