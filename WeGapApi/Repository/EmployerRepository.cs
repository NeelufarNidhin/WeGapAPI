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
        private readonly IMapper _mapper; // Inject AutoMapper here
      

        public EmployerRepository(ApplicationDbContext  context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
           
        }

        public async Task<Guid> AddEmployerAsync(EmployerDto employerDto)
        {

            var employerEntity = _mapper.Map<Employer>(employerDto);
           employerEntity.Id = Guid.NewGuid();
            _context.Employers.Add(employerEntity);
            await _context.SaveChangesAsync();
           // return CreatedAtActionResult(nameof(GetEmployerById), new { id = employerDto.Id});
           return employerEntity.Id;

        }

          

            public Task<bool> DeleteEmployerAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmployerDto>> GetAllEmployersAsync()
        {
            var employers = await _context.Employers.ProjectTo<EmployerDto>(_mapper.ConfigurationProvider).ToListAsync();
            return employers;
        }

        public Employer GetEmployerById(int employerId)
        {
            throw new NotImplementedException();
        }

        public async Task<EmployerDto> GetEmployerByIdAsync(Guid id)
        {
            var employer = await _context.Employers
               .Where(e => e.Id == id)
               .ProjectTo<EmployerDto>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync();

            return employer;
        }

        public Task<UserDto> GetUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateEmployerAsync(Guid id, EmployerDto employerDto)
        {
            var existingEmployer = await _context.Employers.FindAsync(id);

            if (existingEmployer == null)
                return false;

            _mapper.Map(employerDto, existingEmployer);
            await _context.SaveChangesAsync();

            return true;
        }
    }


}

