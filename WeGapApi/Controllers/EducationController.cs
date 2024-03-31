using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
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
        private ApiResponse _response;
        public EducationController(IServiceManager service)
        {
            _service= service;
            _response = new ApiResponse();
        }

        [HttpGet]
     //   [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> GetAllEducation()
            
        {
            try
            {
               var educationDto = await  _service.EducationService.GetAllAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = educationDto;
                return Ok(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
                {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
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
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = educationDto;
                return Ok(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }   

           
        }


        [HttpGet("employee/{id}")]

        //[Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> GetEmployeeEducation(Guid id)
        {
            try
            {
                var educationDto = await _service.EducationService.GetEmployeeEducation(id);

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = educationDto;
                return Ok(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);

            }

        }

        [HttpPost]
       // [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> AddEducation([FromBody] AddEducationDto addEducationDto) 
        {

            try
            {
                var educationDto = await _service.EducationService.AddEducationAsync(addEducationDto);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = educationDto;
                return CreatedAtAction(nameof(GetEducationById), new { id = educationDto.Id }, _response);
            }
            catch (InvalidOperationException ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
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
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = educationDto;
                return Ok(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }

        [HttpDelete("{id}")]
       // [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> DeleteEducation(Guid id)
        {
            try
            {
                var educationDto = await _service.EducationService.DeleteEducationAsync(id);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = educationDto;
                return Ok(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }

    }
}

