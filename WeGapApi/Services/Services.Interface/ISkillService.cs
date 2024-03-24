using System;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

namespace WeGapApi.Services.Services.Interface
{
	public interface ISkillService
	{
        Task<List<SkillDto>> GetAllSkillAsync();
        Task<SkillDto> GetSkillByIdAsync(Guid id);
        Task<SkillDto> AddSkillAsync(AddSkillDto Skill);
        Task<SkillDto> UpdateSkillAsync(Guid id, UpdateSkillDto Skill);
        Task<SkillDto> DeleteSkillAsync(Guid id);
        Task<List<SkillDto>> GetEmployeeSkillAsync(Guid id);
    }
}

