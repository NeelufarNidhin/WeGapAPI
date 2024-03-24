using System;
using AutoMapper;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Repository.Interface;
using WeGapApi.Services.Services.Interface;

namespace WeGapApi.Services
{
	public class SkillService : ISkillService
	{

        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public SkillService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<SkillDto> AddSkillAsync(AddSkillDto addSkillDto)
        {

            var skill = _mapper.Map<Skill>(addSkillDto);

            var skillDomain = await _repositoryManager.Skill.AddSkillAsync(skill);


            var skillDto = _mapper.Map<SkillDto>(skillDomain);
            return skillDto;
        }



        public async Task<List<SkillDto>> GetAllSkillAsync()
        {
            var skillDomain = await _repositoryManager.Skill.GetAllSkillAsync();

            var skillDto = _mapper.Map<List<SkillDto>>(skillDomain);
            return skillDto;
        }

        public async Task<SkillDto> GetSkillByIdAsync(Guid id)
        {
            //obtain data
            var skillDomain = await _repositoryManager.Skill.GetSkillByIdAsync(id);



            //mapping
            var skillDto = _mapper.Map<SkillDto>(skillDomain);
            return skillDto;
        }

        public async Task<SkillDto> UpdateSkillAsync(Guid id, UpdateSkillDto updateSkillDto)
        {


            //Map DTO to Domain model
            var skillDomain = _mapper.Map<Skill>(updateSkillDto);

            //check if employee exists
            skillDomain = await _repositoryManager.Skill.UpdateSkillAsync(id, skillDomain);




            var skillDto = _mapper.Map<SkillDto>(skillDomain);
            return skillDto;
        }

        public async Task<SkillDto> DeleteSkillAsync(Guid id)
        {
            var skillDomain = await _repositoryManager.Skill.DeleteSkillAsync(id);



            var skillDto = _mapper.Map<SkillDto>(skillDomain);
            return skillDto;
        }

        public async Task<List<SkillDto>> GetEmployeeSkillAsync(Guid id)
        {
            var skill = await _repositoryManager.Skill.GetEmployeeSkillAsync(id);

            var skillDto = _mapper.Map<List<SkillDto>>(skill);

            return skillDto;
        }
    }
}


