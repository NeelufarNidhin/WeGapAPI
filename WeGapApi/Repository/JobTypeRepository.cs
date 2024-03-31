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
            var jobTypeFromDb = await _context.JobType.FirstOrDefaultAsync(u => u.JobTypeName == jobType.JobTypeName);

            if(jobTypeFromDb != null)
            {
                throw new InvalidOperationException("Job Type already exists");
            }
            await _context.JobType.AddAsync(jobType);
            _context.SaveChanges();
            return jobType;
        }

        public async Task<JobType> DeleteJobTypeAsync(Guid id)
        {
            var jobTypefromDb = await _context.JobType.FirstOrDefaultAsync(x => x.Id == id);
            if (jobTypefromDb == null)
            {
                throw new InvalidOperationException("Job Type not Found");
            }

            _context.JobType.Remove(jobTypefromDb);

            await _context.SaveChangesAsync();

            return jobTypefromDb;
        }

        public async Task<List<JobType>> GetAllJobTypeAsync()
        {
            return await _context.JobType.ToListAsync();
        }

        public async Task<JobType> GetJobTypeByIdAsync(Guid id)
        {
            var jobTypefromDb = await _context.JobType.FirstOrDefaultAsync(x => x.Id == id);
            if (jobTypefromDb == null)
            {
                throw new InvalidOperationException("Job Type not Found");
            }
            return jobTypefromDb;
        }

        public  async Task<JobType> UpdateJobTypeAsync(Guid id, JobType jobType)
        {
            var jobTypefromDb = await _context.JobType.FirstOrDefaultAsync(x => x.Id == id);

            if (jobTypefromDb == null)
            {
                throw new InvalidOperationException("Job Type not Found");
            }
           
                jobTypefromDb.JobTypeName = jobType.JobTypeName;

                _context.SaveChanges();
           

                return jobTypefromDb;
        }
    }
}

