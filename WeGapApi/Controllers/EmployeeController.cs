using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = SD.Role_Employee + ", " + SD.Role_Employer)]

        public async Task<IActionResult> GetAll()
        {
            try{ 
            var employeeDto = await _serviceManager.EmployeeService.GetAllAsync();

            return Ok(employeeDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }

        }

        [HttpGet("exists/{userId}")]
        [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> EmployeeExisits(string userId)
        {
            try { 
            var employeeDto = await _serviceManager.EmployeeService.EmployeeExists(userId);

            
            return Ok(employeeDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }



        [HttpGet("{id}", Name = "EmployeeById")]
        [Authorize(Roles = SD.Role_Employee + ", " + SD.Role_Employer)]

        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            try
            {

                var employeeDto = await _serviceManager.EmployeeService.GetEmployeeByIdAsync(id);

                return Ok(employeeDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }
       
        [HttpPost]
        [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> AddEmployee([FromForm] AddEmployeeDto addEmployeeDto)

        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

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
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = ex.ToString();

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }

          

           
        }


        [HttpPut("{id}")]
        [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {

            try {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var employeeDto = await _serviceManager.EmployeeService.UpdateEmployeeAsync(id,updateEmployeeDto);

                 return Ok(employeeDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
           try { 
          await  _serviceManager.EmployeeService.DeleteEmployeeAsync(id);

            return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }

        


    }
}

