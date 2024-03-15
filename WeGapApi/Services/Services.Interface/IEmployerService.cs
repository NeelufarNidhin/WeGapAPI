using System;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

namespace WeGapApi.Services.Services.Interface
{
	public interface IEmployerService
	{
        Task<List<EmployerDto>> GetAllEmployerAsync();
        Task<EmployerDto> GetEmployerByIdAsync(Guid id);
        Task<EmployerDto> EmployerExists(string id);
        Task<EmployerDto> AddEmployerAsync(AddEmployerDto employer);
        Task<EmployerDto?> UpdateEmployerAsync(Guid id, UpdateEmployerDto employer);
        Task<EmployerDto?> DeleteEmployerAsync(Guid id);

    }
}

