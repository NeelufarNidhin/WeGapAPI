using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeGapApi.Data;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Services.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {

        private readonly IServiceManager _serviceManager;


        public EmployeeController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var employeeDto = await _serviceManager.EmployeeService.GetAllAsync();

            return Ok(employeeDto);

        }

        [HttpGet("exists/{userId}")]
        public async Task<IActionResult> EmployeeExisits(string userId)
        {
            var employeeDto = await _serviceManager.EmployeeService.EmployeeExists(userId);

            
            return Ok(employeeDto);
        }



        [HttpGet("{id}", Name = "EmployeeById")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
           

          var employeeDto =  await  _serviceManager.EmployeeService.GetEmployeeByIdAsync(id);
           

            return Ok(employeeDto);

        }
       
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeDto addEmployeeDto)

        {


            var employeeDto = _serviceManager.EmployeeService.AddEmployeeAsync(addEmployeeDto);

            return CreatedAtRoute("EmployeeById", new { id = employeeDto.Id }, employeeDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {

          

            var employeeDto = await _serviceManager.EmployeeService.UpdateEmployeeAsync(id,updateEmployeeDto);

            return Ok(employeeDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
          await  _serviceManager.EmployeeService.DeleteEmployeeAsync(id);

            return Ok();
        }

        


    }
}

