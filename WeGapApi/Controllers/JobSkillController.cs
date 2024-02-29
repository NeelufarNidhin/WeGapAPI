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
            var jobSkillDto = await _service.JobSkillService.GetAllJobSkillAsync();

            return Ok(jobSkillDto);


        }


        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetJobSkillById([FromRoute] Guid id)
        {


            var jobSkillDto = await _service.JobSkillService.GetJobSkillByIdAsync(id);
            return Ok(jobSkillDto);


        }


        [HttpPost]
        public async Task<IActionResult> AddJobSKill([FromBody] AddJobSkillDto addJobSkillDto)
        {


            var jobSkillDto = await _service.JobSkillService.AddJobSkillAsync(addJobSkillDto);
            return CreatedAtAction(nameof(GetJobSkillById), new { id = jobSkillDto.Id }, jobSkillDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkillJob(Guid id, [FromBody] UpdateJobSkillDto updateJobSkillDto)
        {


            var jobSkillDto = await _service.JobSkillService.UpdateJobSkillAsync(id, updateJobSkillDto);
            return Ok(jobSkillDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobSKill(Guid id)
        {

            await _service.JobSkillService.DeleteJobSkillAsync(id);
            return Ok();
        }
    }
}

