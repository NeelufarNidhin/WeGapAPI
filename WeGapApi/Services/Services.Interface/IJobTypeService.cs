using System;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

namespace WeGapApi.Services.Services.Interface
{
	public interface IJobTypeService
	{
        Task<List<JobTypeDto>> GetAllJobTypeAsync();
        Task<JobTypeDto> GetJobTypeByIdAsync(Guid id);
        Task<JobTypeDto> AddJobTypeAsync(AddJobTypeDto jobtype);
        Task<JobTypeDto> UpdateJobTypeAsync(Guid id, UpdateJobTypeDto jobType);
        Task<JobTypeDto> DeleteJobTypeAsync(Guid id);
    }
}

