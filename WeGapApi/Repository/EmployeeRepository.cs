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
            if(employee is null)
            {
                throw new InvalidOperationException("Employee not found");
            }

           await _context.Employees.AddAsync(employee);
           await _context.SaveChangesAsync();

            return employee;

        }




        public  async Task<Employee?> DeleteEmployeeAsync(Guid id)
        {
            var employeefromDb = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);


            if (employeefromDb == null)
            {
                throw new InvalidOperationException("Employee not found");
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
            var employeefromDb = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);


            if (employeefromDb == null)
            {
                throw new InvalidOperationException("Employee not found");
            }

            return employeefromDb;
        }

        public async Task<Employee> EmployeeExists (string id)
        {
            var user = _context.Employees.FirstOrDefault(u => u.ApplicationUserId == id);

            if(user is null)
            {
                throw new InvalidOperationException("User not found");
            }
            return user;
        }

        public async Task<Employee?> UpdateEmployeeAsync(Guid id, Employee employee)
        {
            var employeefromDb =  await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
           

            if(employeefromDb == null)
            {
                throw new InvalidOperationException("Employee not found");
            }

            employeefromDb.Address = employee.Address;
            employeefromDb.City = employee.City;
            employeefromDb.DOB = employee.DOB;
            employee.Gender = employee.Gender;
            employeefromDb.MobileNumber = employee.MobileNumber;
            employeefromDb.Pincode = employee.Pincode;
            employeefromDb.State = employee.State;
            employeefromDb.Bio = employee.Bio;
            employeefromDb.ImageName = employee.ImageName;
            _context.Employees.Update(employeefromDb);
            _context.SaveChanges();
            return employeefromDb;

        }
    }
}

