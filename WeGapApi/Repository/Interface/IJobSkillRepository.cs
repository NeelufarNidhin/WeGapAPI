using System;
using WeGapApi.Models;

namespace WeGapApi.Repository.Interface
{
	public interface IJobSkillRepository
	{
        Task<List<JobSkill>> GetAllJobSkillAsync();
        Task<JobSkill> GetJobSkillByIdAsync(int id);
        Task<JobSkill> AddJobSkillAsync(JobSkill jobSkill);
        Task<JobSkill> UpdateJobSkillAsync(int id, JobSkill jobSkill);
        Task<JobSkill> DeleteJobSkillAsync(int id);
    }
}

