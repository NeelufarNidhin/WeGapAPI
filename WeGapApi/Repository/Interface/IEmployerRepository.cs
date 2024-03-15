using System;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

namespace WeGapApi.Data
{
	public interface IEmployerRepository 
	{
        Task<List<Employer>> GetAllEmployerAsync();
        Task<Employer> GetEmployerByIdAsync(Guid id);
        Task<Employer> EmployerExists(string id);
        Task<Employer> AddEmployerAsync(Employer employer);
        Task<Employer?> UpdateEmployerAsync(Guid id, Employer employer);
        Task<Employer?> DeleteEmployerAsync(Guid id);

    }

   
}

