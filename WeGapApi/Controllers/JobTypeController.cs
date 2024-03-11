using System;
using System.Collections.Generic;
using System.Linq;
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

            var jobTypeDto = await _service.JobTypeService.GetAllJobTypeAsync();
            return Ok(jobTypeDto);


        }


        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetJobTypeById([FromRoute] Guid id)
        {
            //obtain data


            var jobTypeDto = await _service.JobTypeService.GetJobTypeByIdAsync(id);
            return Ok(jobTypeDto);


        }


        [HttpPost]
        public async Task<IActionResult> AddJobType([FromBody] AddJobTypeDto addJobTypeDto)
        {


            var jobTypeDto = await _service.JobTypeService.AddJobTypeAsync(addJobTypeDto);
            return CreatedAtAction(nameof(GetJobTypeById), new { id = jobTypeDto.Id }, jobTypeDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobtype(Guid id, [FromBody] UpdateJobTypeDto updateJobTypeDto)
        {


            var jobTypeDto = await _service.JobTypeService.UpdateJobTypeAsync(id, updateJobTypeDto);
            return Ok(jobTypeDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobtype(Guid id)
        {
            await _service.JobTypeService.DeleteJobTypeAsync(id);

            return Ok();
        }
    }
}

