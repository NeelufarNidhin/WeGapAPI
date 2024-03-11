using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Repository.Interface;
using WeGapApi.Services.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    public class JobController : Controller
    {

        private readonly IServiceManager _service;


        public JobController( IServiceManager service)
        {
            _service = service;

        }

        [HttpGet]

        public async Task<IActionResult> GetAllJobs()
        {
            var jobDto = await _service.JobService.GetAllJobsAsync();

            return Ok(jobDto);


        }


        [HttpGet]
        [Route("{id}")]

        public async Task <IActionResult> GetJobById([FromRoute] Guid id)
        {

            var jobDto = await _service.JobService.GetJobsByIdAsync(id);
            return Ok(jobDto);


        }


        [HttpPost]
        public async Task<IActionResult> AddJobs([FromBody] AddJobDto addJobDto)
        {

            var jobDto = await _service.JobService.AddJobsAsync(addJobDto);

                return CreatedAtAction(nameof(GetJobById), new { id = jobDto.Id }, jobDto);
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(Guid id, [FromBody] UpdateJobDto updateJobDto)
        {

            var jobDto = await _service.JobService.UpdateJobsAsync(id, updateJobDto);

            return Ok(jobDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            var jobDto = await _service.JobService.DeleteJobsAsync(id);
                

            return Ok(jobDto);
        }
    }
}

