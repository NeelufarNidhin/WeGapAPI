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
            try
            {
                // Add job to context
                await _context.Jobs.AddAsync(job);

              //  If job has associated job skills
                //if (job.JobJobSkill != null && job.JobJobSkill.Any())
                //{
                    // Iterate over each job skill ID in the input array
                    foreach (var jobSkillId in job.JobJobSkill.Select(jjs => jjs.JobSkillId))
                    {
                        var jobJobSkill = new JobJobSkill
                        {
                            JobId = job.Id, // Ensure that the job ID is properly assigned
                            JobSkillId = jobSkillId
                        };

                        // Add the new JobJobSkill entry to the context
                        _context.JobJobSkill.Add(jobJobSkill);
                    }
                //}

                // Save changes to database
                await _context.SaveChangesAsync();
                return job;
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                // Log or throw the exception as needed
                throw new Exception("Failed to add job with job skills.", ex);
            }
        }


        public async Task<Job> DeleteJobsAsync(Guid id)
        {
            var jobfromDb = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);
            if(jobfromDb is null)
            {
                throw new InvalidOperationException("Job Not found");
            }

            _context.Jobs.Remove(jobfromDb);

            await _context.SaveChangesAsync();

            return jobfromDb;
        }

        public async Task<List<Job>> GetAllJobsAsync()
        {
            return await _context.Jobs.Include(j=>j.Employer).ToListAsync();
        }

        public async Task<Job> GetJobsByIdAsync(Guid id)
        {
            var jobfromDb = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);
            if (jobfromDb is null)
            {
                throw new InvalidOperationException("Job Not found");
            }
            return jobfromDb;
        }

        public async Task<Job> UpdateJobsAsync(Guid id, Job job)
        {
           var jobfromDb = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);

            if(jobfromDb == null)
            {
                throw new InvalidOperationException("Job Not found");
            }

            jobfromDb.JobTitle = job.JobTitle;
            jobfromDb.Description = job.Description;
            _context.Jobs.Update(jobfromDb);

           await _context.SaveChangesAsync();

            return jobfromDb;
        }
    }
}

