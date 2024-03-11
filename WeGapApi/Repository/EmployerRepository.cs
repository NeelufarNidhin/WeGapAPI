using System;
using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

namespace WeGapApi.Data
{
	public class EmployerRepository : IEmployerRepository
	{
        private readonly ApplicationDbContext _context;
       
      

        public EmployerRepository(ApplicationDbContext  context) 
        {
            _context = context;
           
           
        }

        public async Task<Employer> AddEmployerAsync(Employer employer)
        {
            await _context.Employers.AddAsync(employer);


            var userFromDb = _context.ApplicationUsers.FirstOrDefault(x => x.Id == employer.ApplicationUserId);
            userFromDb.IsProfile = true;
            _context.SaveChanges();
            return employer;
        }

        public async Task<Employer?> DeleteEmployerAsync(Guid id)
        {
            var employerfromDb = await _context.Employers.FirstOrDefaultAsync(x => x.Id == id);


            if (employerfromDb == null)
            { 
                return null;
            }

            _context.Employers.Remove(employerfromDb);
            await _context.SaveChangesAsync();
            return employerfromDb;
        }

        public async Task<List<Employer>> GetAllEmployerAsync()
        {
            var employers = await _context.Employers.Include("ApplicationUser").ToListAsync();

            return employers;
        }

        public async Task<Employer> GetEmployerByIdAsync(Guid id)
        {
            return await _context.Employers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employer?> UpdateEmployerAsync(Guid id, Employer employer)
        {
            var employerfromDb = await _context.Employers.FirstOrDefaultAsync(x => x.Id == id);


            if (employerfromDb == null)
            {
                return null;
            }

            employerfromDb.CompanyName = employer.CompanyName;
            employerfromDb.Description = employer.Description;
            employerfromDb.Location = employer.Location;
            employerfromDb.Website = employer.Website;
           

            _context.SaveChanges();
            return employerfromDb;
        }
    }


}

