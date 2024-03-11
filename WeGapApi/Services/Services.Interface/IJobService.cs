using System;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

namespace WeGapApi.Services.Services.Interface
{
	public interface IJobService
	{
        Task<List<JobDto>> GetAllJobsAsync();
        Task<JobDto> GetJobsByIdAsync(Guid id);
        Task<JobDto> AddJobsAsync(AddJobDto job);
        Task<JobDto> UpdateJobsAsync(Guid id, UpdateJobDto job);
        Task<JobDto> DeleteJobsAsync(Guid id);
    }
}

