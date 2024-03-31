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
    
    public class JobTypeController : Controller
    {
       private readonly IServiceManager _service;
        private readonly ApiResponse _response;
        public JobTypeController(IServiceManager service)
        {
            _service = service;
            _response = new ApiResponse();
        }

        [HttpGet]
        [Authorize(Roles = SD.Role_Admin + " ," + SD.Role_Employer)]
        public async Task<IActionResult> GetAllJobType()
        {
            try { 

            var jobTypeDto = await _service.JobTypeService.GetAllJobTypeAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = jobTypeDto;
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
        [Authorize(Roles = SD.Role_Admin + " ," + SD.Role_Employer)]
        public async Task<IActionResult> GetJobTypeById([FromRoute] Guid id)
        {
            //obtain data

            try { 
            var jobTypeDto = await _service.JobTypeService.GetJobTypeByIdAsync(id);

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = jobTypeDto;
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
        [Authorize(Roles = SD.Role_Admin + " ," + SD.Role_Employer)]
        public async Task<IActionResult> AddJobType([FromBody] AddJobTypeDto addJobTypeDto)
        {

            try {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var jobTypeDto = await _service.JobTypeService.AddJobTypeAsync(addJobTypeDto);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = jobTypeDto;
                

                return CreatedAtAction(nameof(GetJobTypeById), new { id = jobTypeDto.Id }, _response);
                
           
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
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> UpdateJobtype(Guid id, [FromBody] UpdateJobTypeDto updateJobTypeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var jobTypeDto = await _service.JobTypeService.UpdateJobTypeAsync(id, updateJobTypeDto);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = jobTypeDto;
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
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> DeleteJobtype(Guid id)
        {
            try { 
         var jobTypeDto=  await _service.JobTypeService.DeleteJobTypeAsync(id);

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = jobTypeDto;
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

