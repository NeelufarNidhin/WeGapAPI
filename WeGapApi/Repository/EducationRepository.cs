using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WeGapApi.Data;
using WeGapApi.Models;
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
           await _context.Education.AddAsync(education);
            _context.SaveChanges();
            return (education);

        }

        public async Task<Education?> DeleteEducationAsync(Guid id)
        {
            var educationFromDb = await _context.Education.FirstOrDefaultAsync(u => u.Id == id);

            if(educationFromDb == null)
            {
                return null;
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
           return  await _context.Education.FirstOrDefaultAsync(u => u.Id == id);
            

        }

        public async Task<Education> UpdateEducationAsync(Guid id, Education education)
        {
            var educationFromDb = await _context.Education.FirstOrDefaultAsync(u => u.Id == id);

            if (educationFromDb == null)
            {
                return null;
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

