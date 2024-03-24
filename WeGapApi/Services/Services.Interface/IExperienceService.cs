using System;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

namespace WeGapApi.Services.Services.Interface
{
	public interface IExperienceService
	{
        Task<List<ExperienceDto>> GetAllExperienceAsync();
        Task<ExperienceDto> GetExperienceByIdAsync(Guid id);
        Task<ExperienceDto> AddExperienceAsync(AddExperienceDto experience);
        Task<ExperienceDto?> UpdateExperienceAsync(Guid id, UpdateExperienceDto experience);
        Task<ExperienceDto?> DeleteExperienceAsync(Guid id);
        Task<List<ExperienceDto>> GetEmployeeExperience(Guid id);
    }
}

