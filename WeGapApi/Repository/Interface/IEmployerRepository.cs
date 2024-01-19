using System;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

namespace WeGapApi.Data
{
	public interface IEmployerRepository 
	{
        Employer GetEmployerById(int employerId);
        Task<IEnumerable<EmployerDto>> GetAllEmployersAsync();
        Task<EmployerDto> GetEmployerByIdAsync(Guid id);
        Task<UserDto> GetUserByIdAsync(String id);
        Task<Guid> AddEmployerAsync(EmployerDto employerDto);
        Task<bool> UpdateEmployerAsync(Guid id, EmployerDto employerDto);
        Task<bool> DeleteEmployerAsync(Guid id);

    }

   
}

