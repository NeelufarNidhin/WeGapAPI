using System;
using WeGapApi.Models;

namespace WeGapApi.Repository.Interface
{
	public interface IJobSkillRepository
	{
        Task<List<JobSkill>> GetAllJobSkillAsync();
        Task<JobSkill> GetJobSkillByIdAsync(Guid id);
        Task<JobSkill> AddJobSkillAsync(JobSkill jobSkill);
        Task<JobSkill> UpdateJobSkillAsync(Guid id, JobSkill jobSkill);
        Task<JobSkill> DeleteJobSkillAsync(Guid id);
        
    }
}

