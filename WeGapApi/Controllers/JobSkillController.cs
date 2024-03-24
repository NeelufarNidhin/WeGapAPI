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
    public class JobSkillController : Controller
    {
        private readonly IServiceManager _service;

        public JobSkillController(IServiceManager service)
        {
            _service = service;

        }

        [HttpGet]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> GetAllJobSKill()

        {
            try
            {
                var jobSkillDto = await _service.JobSkillService.GetAllJobSkillAsync();

                return Ok(jobSkillDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
          


        }


        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> GetJobSkillById([FromRoute] Guid id)
        {

            try
            {
                var jobSkillDto = await _service.JobSkillService.GetJobSkillByIdAsync(id);
                return Ok(jobSkillDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
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
                return CreatedAtAction(nameof(GetJobSkillById), new { id = jobSkillDto.Id }, jobSkillDto);
               
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
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
                    return Ok(jobSkillDto);
                }

           
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> DeleteJobSKill(Guid id)
        {
            try
            {
            await _service.JobSkillService.DeleteJobSkillAsync(id);
            return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

