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
  
    public class JobSkillController : Controller
    {
        private readonly IServiceManager _service;
        private readonly ApiResponse _response;
        public JobSkillController(IServiceManager service)
        {
            _service = service;
            _response = new ApiResponse();

        }

        [HttpGet]
        [Authorize(Roles = SD.Role_Admin + " ," + SD.Role_Employer)]
        public async Task<IActionResult> GetAllJobSKill()

        {
            try
            {
                var jobSkillDto = await _service.JobSkillService.GetAllJobSkillAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = jobSkillDto;
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
        public async Task<IActionResult> GetJobSkillById([FromRoute] Guid id)
        {

            try
            {
                var jobSkillDto = await _service.JobSkillService.GetJobSkillByIdAsync(id);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = jobSkillDto;
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
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> AddJobSKill([FromBody] AddJobSkillDto addJobSkillDto)
        {

            try
            {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var jobSkillDto = await _service.JobSkillService.AddJobSkillAsync(addJobSkillDto);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = jobSkillDto;
               
                return CreatedAtAction(nameof(GetJobSkillById), new { id = jobSkillDto.Id },_response);
               
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
        public async Task<IActionResult> UpdateSkillJob(Guid id, [FromBody] UpdateJobSkillDto updateJobSkillDto)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                
                else
                {
                    var jobSkillDto = await _service.JobSkillService.UpdateJobSkillAsync(id, updateJobSkillDto);
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Result = jobSkillDto;
                    return Ok(_response);
                }

           
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
        public async Task<IActionResult> DeleteJobSKill(Guid id)
        {
            try
            {
           var jobSkillDto = await _service.JobSkillService.DeleteJobSkillAsync(id);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = jobSkillDto;
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

