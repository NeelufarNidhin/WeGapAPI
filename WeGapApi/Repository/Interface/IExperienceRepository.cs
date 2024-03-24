using System;
using WeGapApi.Models;

namespace WeGapApi.Repository.Interface
{
	public interface IExperienceRepository
	{
        Task<List<Experience>> GetAllExperienceAsync();
        Task<Experience> GetExperienceByIdAsync(Guid id);
        Task<List<Experience>> GetEmployeeExperience(Guid employeeid);
        Task<Experience> AddExperienceAsync(Experience experience);
        Task<Experience?> UpdateExperienceAsync(Guid id, Experience experience);
        Task<Experience?> DeleteExperienceAsync(Guid id);
    }
}

