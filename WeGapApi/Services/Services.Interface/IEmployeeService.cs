using System;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

namespace WeGapApi.Services.Services.Interface
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(Guid id);

        Task<EmployeeDto> AddEmployeeAsync(AddEmployeeDto employeeDto);
        Task<EmployeeDto?> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto employeeDto);
        Task<EmployeeDto> DeleteEmployeeAsync(Guid id);
        Task<EmployeeDto> EmployeeExists(string id);
    }
}

