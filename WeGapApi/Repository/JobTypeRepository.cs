using System;
using Microsoft.EntityFrameworkCore;
using WeGapApi.Data;
using WeGapApi.Models;
using WeGapApi.Repository.Interface;

namespace WeGapApi.Repository
{
	public class JobTypeRepository : IJobTypeRepository
	{
        private readonly ApplicationDbContext _context;
        public JobTypeRepository(ApplicationDbContext context)
		{
            _context = context;
		}

        public async Task<JobType> AddJobTypeAsync(JobType jobType)
        {
            await _context.JobType.AddAsync(jobType);
            _context.SaveChanges();
            return jobType;
        }

        public async Task<JobType> DeleteJobTypeAsync(int id)
        {
            var jobfromDb = await _context.JobType.FirstOrDefaultAsync(x => x.Id == id);

            _context.JobType.Remove(jobfromDb);

            await _context.SaveChangesAsync();

            return jobfromDb;
        }

        public async Task<List<JobType>> GetAllJobTypeAsync()
        {
            return await _context.JobType.ToListAsync();
        }

        public async Task<JobType> GetJobTypeByIdAsync(int id)
        {
            return await _context.JobType.FirstOrDefaultAsync(x => x.Id == id);
        }

        public  async Task<JobType> UpdateJobTypeAsync(int id, JobType jobType)
        {
            var jobfromDb = await _context.JobType.FirstOrDefaultAsync(x => x.Id == id);

            if (jobfromDb == null)
            {
                return null;
            }

            jobfromDb.JobTypeName = jobType.JobTypeName;
           
            _context.SaveChanges();

            return jobfromDb;
        }
    }
}

