﻿using System;
using Microsoft.EntityFrameworkCore;
using WeGapApi.Data;
using WeGapApi.Models;
using WeGapApi.Repository.Interface;

namespace WeGapApi.Repository
{
	public class JobSkillRepository : IJobSkillRepository
	{
        private readonly ApplicationDbContext _context;
        public JobSkillRepository(ApplicationDbContext context)
		{
            _context = context;
		}

        public async Task<JobSkill> AddJobSkillAsync(JobSkill jobSkill)
        {
            await _context.JobSkill.AddAsync(jobSkill);

            _context.SaveChanges();
            return jobSkill;
        }

        public async Task<JobSkill> DeleteJobSkillAsync(Guid id)
        {
            var jobSkillfromDb = await _context.JobSkill.FirstOrDefaultAsync(x => x.Id == id);

            _context.JobSkill.Remove(jobSkillfromDb);

            await _context.SaveChangesAsync();

            return jobSkillfromDb;
        }

        public async Task<List<JobSkill>> GetAllJobSkillAsync()
        {
            return await _context.JobSkill.ToListAsync();
        }

        public async Task<JobSkill> GetJobSkillByIdAsync(Guid id)
        {
            return await _context.JobSkill.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<JobSkill> UpdateJobSkillAsync(Guid id, JobSkill jobSkill)
        {
            var jobSkillfromDb = await _context.JobSkill.FirstOrDefaultAsync(x => x.Id == id);

            if (jobSkillfromDb == null)
            {
                return null;
            }

            jobSkillfromDb.SkillName = jobSkill.SkillName;
           

            _context.SaveChanges();

            return jobSkillfromDb;
        }
    }
}

