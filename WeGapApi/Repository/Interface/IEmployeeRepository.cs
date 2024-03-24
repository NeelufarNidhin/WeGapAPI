using System;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

namespace WeGapApi.Data
{
	public interface IEmployeeRepository
	{
        Task <List<Employee>> GetAllAsync();
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task<Employee> EmployeeExists(string id);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee?> UpdateEmployeeAsync(Guid id, Employee employee);
        Task<Employee?> DeleteEmployeeAsync(Guid id);
        
    }
}

