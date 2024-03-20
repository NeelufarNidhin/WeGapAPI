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
    public class JobSkillController : Controller
    {
        private readonly IServiceManager _service;

        public JobSkillController(IServiceManager service)
        {
            _service = service;

        }

        [HttpGet]

        public async Task<IActionResult> GetAllJobSKill()

        {
            try
            {
                var jobSkillDto = await _service.JobSkillService.GetAllJobSkillAsync();

                return Ok(jobSkillDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching Jobskill data");
            }
          


        }


        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetJobSkillById([FromRoute] Guid id)
        {

            try
            {
                var jobSkillDto = await _service.JobSkillService.GetJobSkillByIdAsync(id);
                return Ok(jobSkillDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching Jobskill data");
            }

        }


        [HttpPost]
        public async Task<IActionResult> AddJobSKill([FromBody] AddJobSkillDto addJobSkillDto)
        {

            try
            {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var jobSkillDto = await _service.JobSkillService.AddJobSkillAsync(addJobSkillDto);
                return CreatedAtAction(nameof(GetJobSkillById), new { id = jobSkillDto.Id }, jobSkillDto);
               
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while adding Jobskill data");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkillJob(Guid id, [FromBody] UpdateJobSkillDto updateJobSkillDto)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var jobSkillDto = await _service.JobSkillService.UpdateJobSkillAsync(id, updateJobSkillDto);
                    return Ok(jobSkillDto);
                }
                else
                {
                    return BadRequest("Please provide the details");
                }

           
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while updating Jobskill data");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobSKill(Guid id)
        {
            try
            {
            await _service.JobSkillService.DeleteJobSkillAsync(id);
            return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while deleting Jobskill data");
            }
        }
    }
}

