using System;
using Microsoft.EntityFrameworkCore;
using WeGapApi.Data;
using WeGapApi.Models;
using WeGapApi.Repository.Interface;

namespace WeGapApi.Repository
{
	public class JobRepository : IJobRepository
	{
        private readonly ApplicationDbContext _context;
		public JobRepository(ApplicationDbContext context)
		{
            _context = context;
		}

        public async Task<Job> AddJobsAsync(Job job)
        {
          await _context.Jobs.AddAsync(job);
            _context.SaveChanges();
            return job;
        }

        public async Task<Job> DeleteJobsAsync(Guid id)
        {
            var jobfromDb = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);

            _context.Jobs.Remove(jobfromDb);

            await _context.SaveChangesAsync();

            return jobfromDb;
        }

        public async Task<List<Job>> GetAllJobsAsync()
        {
            return await _context.Jobs.Include(j=>j.Employer).Include(j=>j.JobSkills).Include(j=>j.JobTypes).ToListAsync();
        }

        public async Task<Job> GetJobsByIdAsync(Guid id)
        {
            return await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Job> UpdateJobsAsync(Guid id, Job job)
        {
           var jobfromDb = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);

            if(jobfromDb == null)
            {
                return null;
            }

            jobfromDb.JobTitle = job.JobTitle;
            jobfromDb.Description = job.Description;

            _context.SaveChanges();

            return jobfromDb;
        }
    }
}

