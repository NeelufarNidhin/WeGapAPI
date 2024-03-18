using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeGapApi.Data;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Services.Services.Interface;
using WeGapApi.Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {

        private readonly IServiceManager _serviceManager;
        private readonly ApiResponse _response;
        private readonly IBlobService _blobService;
        public EmployeeController(IServiceManager serviceManager, IBlobService blobService)
        {
            _serviceManager = serviceManager;
            _response = new ApiResponse();
            _blobService = blobService;
           
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try{ 
            var employeeDto = await _serviceManager.EmployeeService.GetAllAsync();

            return Ok(employeeDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching employee data.");

            }

        }

        [HttpGet("exists/{userId}")]
        public async Task<IActionResult> EmployeeExisits(string userId)
        {
            try { 
            var employeeDto = await _serviceManager.EmployeeService.EmployeeExists(userId);

            
            return Ok(employeeDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while checking employee data.");

            }
        }



        [HttpGet("{id}", Name = "EmployeeById")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            try
            {

                var employeeDto = await _serviceManager.EmployeeService.GetEmployeeByIdAsync(id);

                return Ok(employeeDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching employee data.");

            }
        }
       
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromForm] AddEmployeeDto addEmployeeDto)

        {
            try
            {
                if (ModelState.IsValid)
                {
                    //if (addEmployeeDto.Imagefile == null || addEmployeeDto.Imagefile.Length == 0)
                    //{
                    //    return BadRequest();
                    //}

                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(addEmployeeDto.Imagefile.FileName)}";
                addEmployeeDto.ImageName = await _blobService.UploadBlob(fileName, SD.Storage_Container, addEmployeeDto.Imagefile);

                var employeeDto = _serviceManager.EmployeeService.AddEmployeeAsync(addEmployeeDto);
                    _response.Result = employeeDto;
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtRoute("EmployeeById", new { id = employeeDto.Id }, employeeDto);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = "Add required fields";
                    return BadRequest(_response);
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = ex.ToString();

                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while adding employee.");

            }

          

           
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {

            try { 

            var employeeDto = await _serviceManager.EmployeeService.UpdateEmployeeAsync(id,updateEmployeeDto);

            return Ok(employeeDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching employee data.");

            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
           try { 
          await  _serviceManager.EmployeeService.DeleteEmployeeAsync(id);

            return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching deleting data.");

            }
        }

        


    }
}

