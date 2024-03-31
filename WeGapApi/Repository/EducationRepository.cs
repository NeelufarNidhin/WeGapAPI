using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WeGapApi.Data;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Repository.Interface;

namespace WeGapApi.Repository
{
	public class EducationRepository : IEducationRepository

	{
        public readonly ApplicationDbContext _context;
     
		public EducationRepository(ApplicationDbContext context)
		{
            _context = context;
		}
        
        public  async Task<Education> AddEducationAsync(Education education)
        {
              var userFromDb = _context.Education.FirstOrDefault(x => (x.Degree == education.Degree && x.Subject == education.Subject && x.EmployeeId == x.EmployeeId));
            
                if(userFromDb is not null )
                {
                    throw new InvalidOperationException("Education Already Exists");
                }
            
           await _context.Education.AddAsync(education);
            _context.SaveChanges();
            return (education);

        }

        public async Task<Education?> DeleteEducationAsync(Guid id)
        {
            var educationFromDb = await _context.Education.FirstOrDefaultAsync(u => u.Id == id);

            if(educationFromDb == null)
            {
                throw new InvalidOperationException("education id not found");
            }

            _context.Education.Remove(educationFromDb);
            _context.SaveChanges();
            return educationFromDb;
        }

        public async Task<List<Education>> GetAllAsync()
        {
            var education = await _context.Education.ToListAsync();
            return education;
        }

        public async Task<Education> GetEducationByIdAsync(Guid id)
        {
            var educationFromDb = await _context.Education.FirstOrDefaultAsync(u => u.Id == id);

            if (educationFromDb == null)
            {
                throw new InvalidOperationException("education id not found");
            }
            return educationFromDb;
        }

    

        public async Task<List<Education>> GetEmployeeEducation(Guid id)
        {
            var education = _context.Education.Where(u => u.EmployeeId == id).ToList();

            if (education is null)
            {
                throw new InvalidOperationException("education not found");

            }
            return education;
        }

        public async Task<Education> UpdateEducationAsync(Guid id, Education education)
        {
            var educationFromDb = await _context.Education.FirstOrDefaultAsync(u => u.Id == id);

            if (educationFromDb == null)
            {
                throw new InvalidOperationException("education id not found");
            }

            educationFromDb.University = education.University;
            educationFromDb.Degree = education.Degree;
            educationFromDb.Subject = education.Subject;
            educationFromDb.Starting_Date = education.Starting_Date;
            educationFromDb.CompletionDate = education.CompletionDate;
            educationFromDb.Percentage = education.Percentage;

            _context.Education.Update(educationFromDb);
            _context.SaveChanges();
            return educationFromDb;

        }
    }
}

