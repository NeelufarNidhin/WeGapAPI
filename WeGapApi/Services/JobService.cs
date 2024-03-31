using System;
using AutoMapper;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Repository;
using WeGapApi.Repository.Interface;
using WeGapApi.Services.Services.Interface;

namespace WeGapApi.Services
{
	public class JobService: IJobService
	{
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public JobService(IRepositoryManager repositoryManager, IMapper mapper)
		{
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<JobDto> AddJobsAsync(AddJobDto addJobDto)
        {


           var job =  _mapper.Map<Job>(addJobDto);

           

            var jobDomain = await _repositoryManager.Job.AddJobsAsync(job);


            var jobDto = _mapper.Map<JobDto>(jobDomain);
            return jobDto;
        }
            

        public async Task<JobDto> DeleteJobsAsync(Guid id)
        {
            var jobDomain = await _repositoryManager.Job.DeleteJobsAsync(id);

            
            var jobDto = _mapper.Map<JobDto>(jobDomain);
            return jobDto;
        }

        public async Task<List<JobDto>> GetAllJobsAsync()
        {
            var jobDomain = await _repositoryManager.Job.GetAllJobsAsync();

            var jobDto = _mapper.Map<List<JobDto>>(jobDomain);
            return jobDto;
        }

        public async Task<JobDto> GetJobsByIdAsync(Guid id)
        {
            //obtain data
            var jobDomain = await _repositoryManager.Job.GetJobsByIdAsync(id);

            

            //mapping
            var jobDto = _mapper.Map<JobDto>(jobDomain);
            return jobDto;

        }

        public async Task<JobDto> UpdateJobsAsync(Guid id, UpdateJobDto updateJobDto)
        {
            


            //Map DTO to Domain model
            var jobDomain = _mapper.Map<Job>(updateJobDto);

            //check if employee exists
            jobDomain = await _repositoryManager.Job.UpdateJobsAsync(id, jobDomain);

            

            var jobDto = _mapper.Map<JobDto>(jobDomain);
            return jobDto;
        }
    }
}

