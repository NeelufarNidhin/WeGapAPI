using System;
using WeGapApi.Models;

namespace WeGapApi.Repository.Interface
{
	public interface ISkillRepository
	{
        Task<List<Skill>> GetAllSkillAsync();
        Task<Skill> GetSkillByIdAsync(Guid id);
        Task<Skill> AddSkillAsync(Skill skill);
        Task<Skill> UpdateSkillAsync(Guid id, Skill skill);
        Task<Skill> DeleteSkillAsync(Guid id);
        Task<List<Skill>> GetEmployeeSkillAsync(Guid id);
    }
}

