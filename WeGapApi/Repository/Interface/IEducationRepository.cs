using System;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

namespace WeGapApi.Repository.Interface
{
	public interface IEducationRepository
	{
        Task<List<Education>> GetAllAsync();
        Task<Education> GetEducationByIdAsync(Guid id);
        Task<List<Education>> GetEmployeeEducation(Guid id);
        Task<Education> AddEducationAsync(Education education);
        Task<Education?> UpdateEducationAsync(Guid id, Education education);
        Task<Education?> DeleteEducationAsync(Guid id);
    }
}

