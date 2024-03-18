using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeGapApi.Data;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Services;
using WeGapApi.Services.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    public class EmployerController : ControllerBase
    {

        private readonly IServiceManager _service;
        public EmployerController(IServiceManager service)

        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEmployers()
        {

            try { 
            var employerDto = await _service.EmployerService.GetAllEmployerAsync();

            return Ok(employerDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching employer data.");

            }

        }

        [HttpGet("exists/{userId}")]
        public async Task<IActionResult> EmployerExisits(string userId)
        {
            try { 
            var employerDto = await _service.EmployerService.EmployerExists(userId);


            return Ok(employerDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while checking employer data.");

            }
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEmployerById([FromRoute] Guid id)
        {

            try
            {

                var employerDto = await _service.EmployerService.GetEmployerByIdAsync(id);

                return Ok(employerDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching employer data.");

            }

        }



       
        [HttpPost]
        public async Task<IActionResult> AddEmployer([FromBody] AddEmployerDto addEmployerDto)

        {
            try {
                if (ModelState.IsValid)
                {
                    var employerDto = await _service.EmployerService.AddEmployerAsync(addEmployerDto);


                    return CreatedAtAction(nameof(GetEmployerById), new { id = employerDto.Id }, employerDto);
                }
                else{
                    return BadRequest("Please chk the employer credentials");
                }
           
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while adding employer data.");

            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployer(Guid id, [FromBody] UpdateEmployerDto updateEmployerDto)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var employerDto =await  _service.EmployerService.UpdateEmployerAsync(id, updateEmployerDto);

                return Ok(employerDto);
                }
                else
                {
                    return BadRequest("Please chk the employer credentials");
                }

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while updating employer data.");

            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployer(Guid id)
        {
            try { 
            var employerDto = await _service.EmployerService.DeleteEmployerAsync(id);
            return Ok(employerDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while deleting employer data.");

            }
        }


    }
}

