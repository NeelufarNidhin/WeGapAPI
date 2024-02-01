using System;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

namespace WeGapApi.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

       

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
           
           
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
             await _context.Employees.AddAsync(employee);


            var userFromDb = _context.ApplicationUsers.FirstOrDefault(x => x.Id == employee.ApplicationUserId);
            employee.CreatedStatus = true;
            userFromDb.IsProfile = true;
            _context.SaveChanges();
            return (employee);
        }

        public  async Task<Employee?> DeleteEmployeeAsync(Guid id)
        {
            var employeefromDb = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);


            if (employeefromDb == null)
            {
                return null;
            }

           _context.Employees.Remove(employeefromDb);
            await _context.SaveChangesAsync();
            return employeefromDb;
        }

        

        public async Task <List<Employee>> GetAllAsync()
        {
            var employees = await _context.Employees.Include("ApplicationUser").ToListAsync();

            return employees;

        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            return await _context.Employees.Include("ApplicationUser").FirstOrDefaultAsync(x => x.Id == id);
        }

        


       

        public async Task<Employee?> UpdateEmployeeAsync(Guid id, Employee employee)
        {
            var employeefromDb =  await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
           

            if(employeefromDb == null)
            {
                return null;
            }

            employeefromDb.Address = employee.Address;
            employeefromDb.City = employee.City;
            employeefromDb.DOB = employee.DOB;
            employeefromDb.MobileNumber = employee.MobileNumber;
            employeefromDb.Pincode = employee.Pincode;
            employeefromDb.State = employee.State;

            _context.SaveChanges();
            return employeefromDb;

        }
    }
}

