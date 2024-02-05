using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeGapApi.Data;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;


        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var employees = await _employeeRepository.GetAllAsync();

            var employeeDto = _mapper.Map<List<EmployeeDto>>(employees);

            return Ok(employeeDto);

        }




        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] Guid id)
        {
            //get data from Database
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);


            //return DTO usimg Mapper
            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            if (employee is null)
                return NotFound();

            return Ok(employeeDto);

        }
        // GET: api/values
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeDto addemployeeDto)

        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeDomain = _mapper.Map<Employee>(addemployeeDto);
            var userDomain = _employeeRepository.GetEmployeeByIdAsync;

            var EmployeeDomain = await _employeeRepository.AddEmployeeAsync(employeeDomain);

            var employeeDto = _mapper.Map<EmployeeDto>(EmployeeDomain);



            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeDto.Id }, employeeDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {

            //validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            //Map DTO to Domain model
            var employeeDomain = _mapper.Map<Employee>(updateEmployeeDto);

            //check if employee exists
            employeeDomain = await _employeeRepository.UpdateEmployeeAsync(id, employeeDomain);

            if (employeeDomain == null)
                return NotFound();


            var employeeDto = _mapper.Map<EmployeeDto>(employeeDomain);

            return Ok(employeeDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employeeDomain = await _employeeRepository.DeleteEmployeeAsync(id);

            if (employeeDomain == null)
                return NotFound();

            return Ok(_mapper.Map<EmployeeDto>(employeeDomain));
        }

        //[HttpGet("Get-User")]
        //public async Task<IActionResult> GetUser(string id)
        //{
        //    var user = await _employeeRepository.GetUserByIdAsync(id);
        //    return Ok(user);
        //}


    }
}

