using System;
using AutoMapper;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Repository.Interface;
using WeGapApi.Services.Services.Interface;

namespace WeGapApi.Services
{
	public class EmployeeService : IEmployeeService
	{
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public EmployeeService(IRepositoryManager repositoryManager, IMapper mapper)
		{
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> AddEmployeeAsync(AddEmployeeDto addemployeeDto)
        {
           
            var employeeDomain = _mapper.Map<Employee>(addemployeeDto);
            var userDomain = _repositoryManager.Employee.GetEmployeeByIdAsync;

            var EmployeeDomain = await _repositoryManager.Employee.AddEmployeeAsync(employeeDomain);

            var employeeDto = _mapper.Map<EmployeeDto>(EmployeeDomain);
            return employeeDto;

        }

        public async Task<EmployeeDto> DeleteEmployeeAsync(Guid id)
        {
            var employeeDomain = await _repositoryManager.Employee.DeleteEmployeeAsync(id);
          var employeeDto =   _mapper.Map<EmployeeDto>(employeeDomain);
            return employeeDto;

        }

        public async  Task<List<EmployeeDto>> GetAllAsync()
        {
            var employees = await _repositoryManager.Employee.GetAllAsync();

            var employeeDto = _mapper.Map<List<EmployeeDto>>(employees);
            return employeeDto;
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _repositoryManager.Employee.GetEmployeeByIdAsync(id);

            
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;


        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {

            //Map DTO to Domain model
            var employeeDomain = _mapper.Map<Employee>(updateEmployeeDto);

            //check if employee exists
            employeeDomain = await _repositoryManager.Employee.UpdateEmployeeAsync(id, employeeDomain);

            //if (employeeDomain == null)
            //    return NotFound();


            var employeeDto = _mapper.Map<EmployeeDto>(employeeDomain);
            return employeeDto;
        }
    }
}

