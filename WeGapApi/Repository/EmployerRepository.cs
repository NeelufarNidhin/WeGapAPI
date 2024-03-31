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

            var employeeFromDb = _context.Employers.FirstOrDefault(x => x.CompanyName == employer.CompanyName);

            if(employeeFromDb is not null)
            {
                throw new Exception("Company Name alreay exists");
            }
            await _context.Employers.AddAsync(employer);
            
            _context.SaveChanges();
            return employer;
        }

        public async Task<Employer?> DeleteEmployerAsync(Guid id)
        {
            var employerfromDb = await _context.Employers.FirstOrDefaultAsync(x => x.Id == id);


            if (employerfromDb == null)
            {
                throw new Exception("Employer doesn't exists");
            }

            _context.Employers.Remove(employerfromDb);
            await _context.SaveChangesAsync();
            return employerfromDb;
        }

        public async Task<Employer> EmployerExists(string id)
        {
            var user = await _context.Employers.FirstOrDefaultAsync(u => u.ApplicationUserId == id);
            return user;
        }

        public async Task<List<Employer>> GetAllEmployerAsync()
        {
            var employers = await _context.Employers.Include("ApplicationUser").ToListAsync();

            return employers;
        }

        public async Task<Employer> GetEmployerByIdAsync(Guid id)
        {
           var employerFromDb= await _context.Employers.FirstOrDefaultAsync(x => x.Id == id);
            if (employerFromDb == null)
            {
                throw new Exception("Employer doesn't exists");
            }

            return employerFromDb;

        }

        public async Task<Employer?> UpdateEmployerAsync(Guid id, Employer employer)
        {
            var employerfromDb = await _context.Employers.FirstOrDefaultAsync(x => x.Id == id);


            if (employerfromDb == null)
            {
                throw new Exception("Employer doesn't exists");

            }

            employerfromDb.CompanyName = employer.CompanyName;
            employerfromDb.Description = employer.Description;
            employerfromDb.Location = employer.Location;
            employerfromDb.Website = employer.Website;

            _context.Employers.Update(employerfromDb);
            _context.SaveChanges();
            return employerfromDb;
        }
    }


}

