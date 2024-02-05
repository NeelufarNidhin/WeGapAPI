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
    public class JobSkillController : Controller
    {
        private readonly IJobSkillRepository _jobSkillRepository;
        private IMapper _mapper;


        public JobSkillController(IJobSkillRepository jobSkillRepository, IMapper mapper)
        {
            _jobSkillRepository = jobSkillRepository;
            _mapper = mapper;

        }

        [HttpGet]

        public async Task<IActionResult> GetAllJobSKill()
        {
            var jobSkillDomain = await _jobSkillRepository.GetAllJobSkillAsync();

            var jobSkillDto = _mapper.Map<List<JobSkillDto>>(jobSkillDomain);

            return Ok(jobSkillDto);


        }


        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetJobSkillById([FromRoute] Guid id)
        {
            //obtain data
            var jobSkillDomain = await _jobSkillRepository.GetJobSkillByIdAsync(id);

            if (jobSkillDomain == null)
            {
                return NotFound();
            }

            //mapping
            var jobSkillDto = _mapper.Map<JobSkillDto>(jobSkillDomain);


            return Ok(jobSkillDto);


        }


        [HttpPost]
        public async Task<IActionResult> AddJobSKill([FromBody] AddJobSkillDto addJobSkillDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var job = _mapper.Map<JobSkill>(addJobSkillDto);

            var jobSkillDomain = await _jobSkillRepository.AddJobSkillAsync(job);


            var jobSkillDto = _mapper.Map<JobSkillDto>(jobSkillDomain);

            return CreatedAtAction(nameof(GetJobSkillById), new { id = jobSkillDto.Id }, jobSkillDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkillJob(Guid id, [FromBody] UpdateJobSkillDto updateJobSkillDto)
        {

            //validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            //Map DTO to Domain model
            var jobSkillDomain = _mapper.Map<JobSkill>(updateJobSkillDto);

            //check if employee exists
            jobSkillDomain = await _jobSkillRepository.UpdateJobSkillAsync(id, jobSkillDomain);

            if (jobSkillDomain == null)
                return NotFound();


            var jobSkillDto = _mapper.Map<JobSkillDto>(jobSkillDomain);

            return Ok(jobSkillDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobSKill(Guid id)
        {
            var jobSkillDomain = await _jobSkillRepository.DeleteJobSkillAsync(id);

            if (jobSkillDomain == null)
                return NotFound();

            return Ok(_mapper.Map<JobSkillDto>(jobSkillDomain));
        }
    }
}

