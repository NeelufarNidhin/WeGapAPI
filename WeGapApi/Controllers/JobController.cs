using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Repository.Interface;
using WeGapApi.Services.Services.Interface;
using WeGapApi.Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
  
    public class JobController : Controller
    {

        private readonly IServiceManager _service;
        private readonly ApiResponse _response;

        public JobController( IServiceManager service)
        {
            _service = service;
            _response = new ApiResponse();

        }

        [HttpGet]
        [Authorize(Roles = SD.Role_Employer +" ," + SD.Role_Employee)]
        public async Task<IActionResult> GetAllJobs()
        {
            try { 
            var jobDto = await _service.JobService.GetAllJobsAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = jobDto;
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
        [Authorize(Roles = SD.Role_Employer + " ," + SD.Role_Employee)]
        public async Task <IActionResult> GetJobById([FromRoute] Guid id)
        {
            try { 

            var jobDto = await _service.JobService.GetJobsByIdAsync(id);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = jobDto;
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
        [Authorize(Roles = SD.Role_Employer)]
        public async Task<IActionResult> AddJobs([FromBody] AddJobDto addJobDto)
        {
            try {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var jobDto = await _service.JobService.AddJobsAsync(addJobDto);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = jobDto;
               
                return CreatedAtAction(nameof(GetJobById), new { id = jobDto.Id }, _response);
                
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
        [Authorize(Roles = SD.Role_Employer)]
        public async Task<IActionResult> UpdateJob(Guid id, [FromBody] UpdateJobDto updateJobDto)
        {
            try {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var jobDto = await _service.JobService.UpdateJobsAsync(id, updateJobDto);

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = jobDto;
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
        [Authorize(Roles = SD.Role_Employer)]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            try { 
            var jobDto = await _service.JobService.DeleteJobsAsync(id);

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = jobDto;
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

