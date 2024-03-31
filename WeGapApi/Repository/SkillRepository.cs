using System;
using Microsoft.EntityFrameworkCore;
using WeGapApi.Data;
using WeGapApi.Models;
using WeGapApi.Repository.Interface;

namespace WeGapApi.Repository
{
    public class SkillRepository : ISkillRepository
    {
        private readonly ApplicationDbContext _context;
        public SkillRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Skill> AddSkillAsync(Skill skill)
        {
            var skillfromDb = await _context.Skill.FirstOrDefaultAsync(u => (u.SkillName == skill.SkillName && u.EmployeeId==skill.EmployeeId));


            if (skillfromDb != null)
            {
                throw new InvalidOperationException("Job Skill name already exists");
            }

            await _context.Skill.AddAsync(skill);
            _context.SaveChanges();
            return skill;
        }

        public async Task<Skill> DeleteSkillAsync(Guid id)
        {
            var skillfromDb = await _context.Skill.FirstOrDefaultAsync(x => x.Id == id);
            if (skillfromDb == null)
            {
                throw new InvalidOperationException("Job skill not Found");
            }

            _context.Skill.Remove(skillfromDb);

            await _context.SaveChangesAsync();

            return skillfromDb;
        }

        public async Task<List<Skill>> GetAllSkillAsync()
        {
            var skill = await _context.Skill.ToListAsync();
            if (skill == null)
            {
                throw new InvalidOperationException("Job skill not Found");
            }

            return skill;
        }

        public async Task<List<Skill>> GetEmployeeSkillAsync(Guid id)
        {

            var skill =  _context.Skill.Where(u => u.EmployeeId == id).ToList();

            if (skill is null)
            {
                throw new InvalidOperationException("skill not found");

            }
            return skill;
        }

        public async Task<Skill> GetSkillByIdAsync(Guid id)
        {
            var skillfromDb = await _context.Skill.FirstOrDefaultAsync(x => x.Id == id);

            if (skillfromDb == null)
            
                throw new InvalidOperationException(" skill not Found");
            
            return skillfromDb;
        }

        public async Task<Skill> UpdateSkillAsync(Guid id, Skill skill)
        {
            var skillfromDb = await _context.Skill.FirstOrDefaultAsync(x => x.Id == id);

            if (skillfromDb == null)
            {
                throw new InvalidOperationException("Job skill not Found");
            }

            skillfromDb.SkillName = skill.SkillName;


            _context.SaveChanges();

            return skillfromDb;
        }
    }
}

