using System;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

namespace WeGapApi.Services.Services.Interface
{
	public interface IJobSkillService
	{
        Task<List<JobSkillDto>> GetAllJobSkillAsync();
        Task<JobSkillDto> GetJobSkillByIdAsync(Guid id);
        Task<JobSkillDto> AddJobSkillAsync(AddJobSkillDto jobSkill);
        Task<JobSkillDto> UpdateJobSkillAsync(Guid id, UpdateJobSkillDto jobSkill);
        Task<JobSkillDto> DeleteJobSkillAsync(Guid id);
    }
}

