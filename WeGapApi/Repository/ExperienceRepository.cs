using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WeGapApi.Data;
using WeGapApi.Models;
using WeGapApi.Repository.Interface;

namespace WeGapApi.Repository
{
    public class ExperienceRepository : IExperienceRepository
    {

        private readonly ApplicationDbContext _context;
       

        public ExperienceRepository(ApplicationDbContext context)
        {
            _context = context;
           
        }

        public async Task<Experience> AddExperienceAsync(Experience experience)
        {
             var userFromDb = _context.Experience.FirstOrDefault(x => (x.CompanyName == experience.CompanyName && x.EmployeeId == experience.EmployeeId));
            
                if(userFromDb is not null )
                {
                    throw new InvalidOperationException("Experience Already Exists");
                }
            
            await _context.Experience.AddAsync(experience);

            _context.SaveChanges();
            return experience;
        }

        public async Task<Experience> DeleteExperienceAsync(Guid id)
        {
            var experiencefromDb = await _context.Experience.FirstOrDefaultAsync(x => x.Id == id);


            if (experiencefromDb == null)
            {
                throw new InvalidOperationException("Experience Not found");
            }

            _context.Experience.Remove(experiencefromDb);
            await _context.SaveChangesAsync();
            return experiencefromDb;
        }

        public async Task<List<Experience>> GetEmployeeExperience( Guid id)
        {
            var experience = _context.Experience.Where(u => u.EmployeeId == id).ToList();

            if (experience is null)
            {
                throw new InvalidOperationException("experince not found");

            }
            return experience;
        }

        public async Task<List<Experience>> GetAllExperienceAsync()
        {
            var experience = await _context.Experience.ToListAsync();

            return experience;
        }

        public async Task<Experience> GetExperienceByIdAsync(Guid id)
        {
          var experiencefromDb = await _context.Experience.FirstOrDefaultAsync(x => x.Id == id);
            if (experiencefromDb == null)
            {
                throw new InvalidOperationException("Experience Not found");
            }
            return experiencefromDb;
        }

        public async Task<Experience> UpdateExperienceAsync(Guid id, Experience experience)
        {
            var experiencefromDb = await _context.Experience.FirstOrDefaultAsync(x => x.Id == id);


            if (experiencefromDb == null)
            {
                throw new InvalidOperationException("Experience Not found");
            }

            experiencefromDb.CurrentJobTitle = experience.CurrentJobTitle;
            experiencefromDb.CompanyName = experience.CompanyName;
            experiencefromDb.Starting_Date = experience.Starting_Date;
            experiencefromDb.CompletionDate = experience.CompletionDate;
            experiencefromDb.IsWorking = experience.IsWorking;
            experiencefromDb.Description = experience.Description;
           // experiencefromDb.EmployeeId = experience.EmployeeId;
            _context.Experience.Update(experiencefromDb);
 

            _context.SaveChanges();
            return experiencefromDb;
        }
    }
}

