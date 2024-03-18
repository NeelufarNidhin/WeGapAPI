using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Repository.Interface;
using WeGapApi.Services.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    public class JobTypeController : Controller
    {
       private readonly IServiceManager _service;

        public JobTypeController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllJobType()
        {
            try { 

            var jobTypeDto = await _service.JobTypeService.GetAllJobTypeAsync();
            return Ok(jobTypeDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetchng job type data.");

            }


        }


        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetJobTypeById([FromRoute] Guid id)
        {
            //obtain data

            try { 
            var jobTypeDto = await _service.JobTypeService.GetJobTypeByIdAsync(id);
            return Ok(jobTypeDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching job type data.");

            }


        }


        [HttpPost]
        public async Task<IActionResult> AddJobType([FromBody] AddJobTypeDto addJobTypeDto)
        {

            try {
                if (ModelState.IsValid)
                {
                    var jobTypeDto = await _service.JobTypeService.AddJobTypeAsync(addJobTypeDto);
                    return CreatedAtAction(nameof(GetJobTypeById), new { id = jobTypeDto.Id }, jobTypeDto);
                }
                else
                {
                    return BadRequest("Please check Job type credentials");
                }
           
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while adding job type data.");

            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobtype(Guid id, [FromBody] UpdateJobTypeDto updateJobTypeDto)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var jobTypeDto = await _service.JobTypeService.UpdateJobTypeAsync(id, updateJobTypeDto);
                    return Ok(jobTypeDto);
                }
                else
                {
                    return BadRequest("Please check Job type credentials");
                }

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while updating job type data.");

            }

}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobtype(Guid id)
        {
            try { 
            await _service.JobTypeService.DeleteJobTypeAsync(id);

            return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while deleting job type data.");

            }
        }
    }
}

