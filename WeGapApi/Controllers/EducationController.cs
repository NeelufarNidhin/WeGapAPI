using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Repository.Interface;
using WeGapApi.Services.Services.Interface;
using WeGapApi.Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = SD.Role_Employee)]
    public class EducationController : Controller
    {
        public readonly IServiceManager _service;
        public EducationController(IServiceManager service)
        {
            _service= service;
        }

        [HttpGet]
     //   [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> GetAllEducation()
            
        {
            try
            {
               var educationDto = await  _service.EducationService.GetAllAsync();
                return Ok(educationDto);
            }
            catch (Exception ex)
                {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
           
        }

        [HttpGet]
        [Route("{id}")]
       // [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> GetEducationById([FromRoute] Guid id)
        {
            try
            {
                var educationDto = await _service.EducationService.GetEducationByIdAsync(id);
                return Ok(educationDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }   

           
        }


        [HttpGet("employee/{id}")]

        //[Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> GetEmployeeEducation(Guid id)
        {
            try
            {
                var educationDto = await _service.EducationService.GetEmployeeEducation(id);

                return Ok(educationDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }

        }

        [HttpPost]
       // [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> AddEducation([FromBody] AddEducationDto addEducationDto) 
        {

            try
            {
                var educationDto = await _service.EducationService.AddEducationAsync(addEducationDto);
                return Ok(educationDto);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
      //  [Authorize(Roles = SD.Role_Employee)]
        public async Task <IActionResult> UpdateEducation(Guid id , [FromBody] UpdateEducationDto updateEducationDto)
        {
            try
            {

                var educationDto = await _service.EducationService.UpdateEducationAsync(id,updateEducationDto);
                if(educationDto is null)
                {
                    return BadRequest("" +
                        "education cannot be null");
                }
                return Ok(educationDto);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
       // [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> DeleteEducation(Guid id)
        {
            try
            {
                var educationDto = await _service.EducationService.DeleteEducationAsync(id);
                return Ok(educationDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}

