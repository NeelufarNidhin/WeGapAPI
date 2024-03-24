using System;
using AutoMapper;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Repository;
using WeGapApi.Repository.Interface;
using WeGapApi.Services.Services.Interface;

namespace WeGapApi.Services
{
	public class JobSkillService : IJobSkillService
	{
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public JobSkillService(IRepositoryManager repositoryManager, IMapper mapper)
		{
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<JobSkillDto> AddJobSkillAsync(AddJobSkillDto addJobSkillDto)
        {
            
            var job = _mapper.Map<JobSkill>(addJobSkillDto);

            var jobSkillDomain = await _repositoryManager.JobSkill.AddJobSkillAsync(job);


            var jobSkillDto = _mapper.Map<JobSkillDto>(jobSkillDomain);
            return jobSkillDto;
        }

        

        public async Task<List<JobSkillDto>> GetAllJobSkillAsync()
        {
            var jobSkillDomain = await _repositoryManager.JobSkill.GetAllJobSkillAsync();

            var jobSkillDto = _mapper.Map<List<JobSkillDto>>(jobSkillDomain);
            return jobSkillDto;
        }

        public async Task<JobSkillDto> GetJobSkillByIdAsync(Guid id)
        {
            //obtain data
            var jobSkillDomain = await _repositoryManager.JobSkill.GetJobSkillByIdAsync(id);

           

            //mapping
            var jobSkillDto = _mapper.Map<JobSkillDto>(jobSkillDomain);
            return jobSkillDto;
        }

        public async Task<JobSkillDto> UpdateJobSkillAsync(Guid id, UpdateJobSkillDto updateJobSkillDto)
        {
            

            //Map DTO to Domain model
            var jobSkillDomain = _mapper.Map<JobSkill>(updateJobSkillDto);

            //check if employee exists
            jobSkillDomain = await _repositoryManager.JobSkill.UpdateJobSkillAsync(id, jobSkillDomain);

            


            var jobSkillDto = _mapper.Map<JobSkillDto>(jobSkillDomain);
            return jobSkillDto;
        }

        public async Task<JobSkillDto> DeleteJobSkillAsync(Guid id)
        {
            var jobSkillDomain = await _repositoryManager.JobSkill.DeleteJobSkillAsync(id);

           

            var jobSkillDto = _mapper.Map<JobSkillDto>(jobSkillDomain);
                 return jobSkillDto;
        }
    }
}

