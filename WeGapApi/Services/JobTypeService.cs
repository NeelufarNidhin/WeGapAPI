using System;
using AutoMapper;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Repository;
using WeGapApi.Repository.Interface;
using WeGapApi.Services.Services.Interface;

namespace WeGapApi.Services
{
	public class JobTypeService : IJobTypeService
	{
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public JobTypeService(IRepositoryManager repositoryManager, IMapper mapper)
		{
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<JobTypeDto> AddJobTypeAsync(AddJobTypeDto addJobTypeDto)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            var jobType = _mapper.Map<JobType>(addJobTypeDto);

            var jobTypeDomain = await _repositoryManager.JobType.AddJobTypeAsync(jobType);


            var jobTypeDto = _mapper.Map<JobTypeDto>(jobTypeDomain);
            return jobTypeDto;

        }

       

        public async Task<List<JobTypeDto>> GetAllJobTypeAsync()
        {
            var jobTypeDomain = await _repositoryManager.JobType.GetAllJobTypeAsync();

            var jobTypeDto = _mapper.Map<List<JobTypeDto>>(jobTypeDomain);
            return jobTypeDto;
        }

        public  async Task<JobTypeDto> GetJobTypeByIdAsync(Guid id)
        {
            var jobTypeDomain = await _repositoryManager.JobType.GetJobTypeByIdAsync(id);

            //if (jobTypeDomain == null)
            //{
            //    return NotFound();
            //}

            //mapping
            var jobTypeDto = _mapper.Map<JobTypeDto>(jobTypeDomain);
            return jobTypeDto;
        }

        public async Task<JobTypeDto> UpdateJobTypeAsync(Guid id, UpdateJobTypeDto updateJobTypeDto)
        {
            ////validation
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);


            //Map DTO to Domain model
            var jobTypeDomain = _mapper.Map<JobType>(updateJobTypeDto);

            //check if employee exists
            jobTypeDomain = await _repositoryManager.JobType.UpdateJobTypeAsync(id, jobTypeDomain);

            //if (jobTypeDomain == null)
            //    return NotFound();


            var jobTypeDto = _mapper.Map<JobTypeDto>(jobTypeDomain);
            return jobTypeDto;
        }

        public async Task<JobTypeDto> DeleteJobTypeAsync(Guid id)
        {
            var jobTypeDomain = await _repositoryManager.JobType.DeleteJobTypeAsync(id);

            //if (jobTypeDomain == null)
            //    return NotFound();

         var jobTypeDto =    _mapper.Map<JobTypeDto>(jobTypeDomain);
            return jobTypeDto;


        }
    }
}

