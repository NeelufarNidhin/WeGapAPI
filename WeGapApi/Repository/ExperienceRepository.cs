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
             var userFromDb = _context.Experience.FirstOrDefault(x => x.CompanyName == experience.CompanyName);
            {
                if(userFromDb is not null )
                {
                    throw new Exception("Experience Already Exists");
                }
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
                throw new Exception("Experience Not found");
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
                throw new Exception("experince not found");

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
            return await _context.Experience.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Experience> UpdateExperienceAsync(Guid id, Experience experience)
        {
            var experiencefromDb = await _context.Experience.FirstOrDefaultAsync(x => x.Id == id);


            if (experiencefromDb == null)
            {
                throw new Exception("Experience Not found");
            }

            //experiencefromDb.CurrentJobTitle = experience.CurrentJobTitle;
            //experiencefromDb.CompanyName = experience.CompanyName;
            //experiencefromDb.Starting_Date = experience.Starting_Date;
            //experiencefromDb.CompletionDate = experience.CompletionDate;
            //experiencefromDb.IsWorking = experience.IsWorking;
            //experiencefromDb.Description = experience.Description;
             _context.Experience.Update(experience);
 

            _context.SaveChanges();
            return experiencefromDb;
        }
    }
}

