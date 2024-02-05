using System;
using WeGapApi.Models;

namespace WeGapApi.Repository.Interface
{
	public interface IJobTypeRepository
	{
        Task<List<JobType>> GetAllJobTypeAsync();
        Task<JobType> GetJobTypeByIdAsync(Guid id);
        Task<JobType> AddJobTypeAsync(JobType jobtype);
        Task<JobType> UpdateJobTypeAsync(Guid id, JobType jobType);
        Task<JobType> DeleteJobTypeAsync(Guid id);
    }
}

