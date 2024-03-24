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
    [Authorize(Roles = SD.Role_Admin)]
    public class JobTypeController : Controller
    {
       private readonly IServiceManager _service;

        public JobTypeController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> GetAllJobType()
        {
            try { 

            var jobTypeDto = await _service.JobTypeService.GetAllJobTypeAsync();
            if(jobTypeDto is null)
                {
                    return NotFound("JobType is Empty ");
                }
            return Ok(jobTypeDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }


        }


        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> GetJobTypeById([FromRoute] Guid id)
        {
            //obtain data

            try { 
            var jobTypeDto = await _service.JobTypeService.GetJobTypeByIdAsync(id);
                
                return Ok(jobTypeDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }


        }


        [HttpPost]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> AddJobType([FromBody] AddJobTypeDto addJobTypeDto)
        {

            try {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var jobTypeDto = await _service.JobTypeService.AddJobTypeAsync(addJobTypeDto);
                if(addJobTypeDto is null)
                {
                    return NotFound("Job Credentials not found");
                }
                    return CreatedAtAction(nameof(GetJobTypeById), new { id = jobTypeDto.Id }, jobTypeDto);
                
           
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

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
                    return Ok(jobTypeDto);
               
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }

}

        [HttpDelete("{id}")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> DeleteJobtype(Guid id)
        {
            try { 
            await _service.JobTypeService.DeleteJobTypeAsync(id);

            return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }
    }
}

