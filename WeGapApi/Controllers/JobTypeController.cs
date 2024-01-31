using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Repository.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    public class JobTypeController : Controller
    {
        private readonly IJobTypeRepository _jobTypeRepository;
        private IMapper _mapper;


        public JobTypeController(IJobTypeRepository jobTypeRepository, IMapper mapper)
        {
            _jobTypeRepository = jobTypeRepository;
            _mapper = mapper;

        }

        [HttpGet]

        public async Task<IActionResult> GetAllJobType()
        {
            var jobTypeDomain = await _jobTypeRepository.GetAllJobTypeAsync();

            var jobTypeDto = _mapper.Map<List<JobTypeDto>>(jobTypeDomain);

            return Ok(jobTypeDto);


        }


        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetJobTypeById([FromRoute] int id)
        {
            //obtain data
            var jobTypeDomain = await _jobTypeRepository.GetJobTypeByIdAsync(id);

            if (jobTypeDomain == null)
            {
                return NotFound();
            }

            //mapping
            var jobTypeDto = _mapper.Map<JobTypeDto>(jobTypeDomain);


            return Ok(jobTypeDto);


        }


        [HttpPost]
        public async Task<IActionResult> AddJobType([FromBody] AddJobTypeDto addJobTypeDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var jobType = _mapper.Map<JobType>(addJobTypeDto);

            var jobTypeDomain = await _jobTypeRepository.AddJobTypeAsync(jobType);


            var jobTypeDto = _mapper.Map<JobTypeDto>(jobTypeDomain);

            return CreatedAtAction(nameof(GetJobTypeById), new { id = jobTypeDto.Id }, jobTypeDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobtype(int id, [FromBody] UpdateJobTypeDto updateJobTypeDto)
        {

            //validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            //Map DTO to Domain model
            var jobTypeDomain = _mapper.Map<JobType>(updateJobTypeDto);

            //check if employee exists
            jobTypeDomain = await _jobTypeRepository.UpdateJobTypeAsync(id, jobTypeDomain);

            if (jobTypeDomain == null)
                return NotFound();


            var jobTypeDto = _mapper.Map<JobTypeDto>(jobTypeDomain);

            return Ok(jobTypeDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobtype(int id)
        {
            var jobTypeDomain = await _jobTypeRepository.DeleteJobTypeAsync(id);

            if (jobTypeDomain == null)
                return NotFound();

            return Ok(_mapper.Map<JobTypeDto>(jobTypeDomain));
        }
    }
}

