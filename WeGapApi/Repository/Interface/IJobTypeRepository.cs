using System;
using WeGapApi.Models;

namespace WeGapApi.Repository.Interface
{
	public interface IJobTypeRepository
	{
        Task<List<JobType>> GetAllJobTypeAsync();
        Task<JobType> GetJobTypeByIdAsync(int id);
        Task<JobType> AddJobTypeAsync(JobType jobtype);
        Task<JobType> UpdateJobTypeAsync(int id, JobType jobType);
        Task<JobType> DeleteJobTypeAsync(int id);
    }
}

