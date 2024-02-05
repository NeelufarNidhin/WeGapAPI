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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    public class JobController : Controller
    {

        private readonly IJobRepository _jobRepository;
        private IMapper _mapper;


        public JobController(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;

        }

        [HttpGet]

        public async Task<IActionResult> GetAllJobs()
        {
            var jobDomain = await _jobRepository.GetAllJobsAsync();

            var jobDto = _mapper.Map<List<JobDto>>(jobDomain);

            return Ok(jobDto);


        }


        [HttpGet]
        [Route("{id}")]

        public async Task <IActionResult> GetJobById([FromRoute] Guid id)
        {
            //obtain data
            var jobDomain = await _jobRepository.GetJobsByIdAsync(id);

            if( jobDomain == null)
            {
                return NotFound();
            }

            //mapping
            var jobDto = _mapper.Map<JobDto>(jobDomain);


            return Ok(jobDto);


        }


        [HttpPost]
        public async Task<IActionResult> AddJobs([FromBody] AddJobDto addJobDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {

                var job = _mapper.Map<Job>(addJobDto);

                var jobDomain = await _jobRepository.AddJobsAsync(job);


                var jobDto = _mapper.Map<JobDto>(jobDomain);

                return CreatedAtAction(nameof(GetJobById), new { id = jobDto.Id }, jobDto);
            }
            catch (DbUpdateException ex)
            {
                // Log the outer exception
                Console.WriteLine($"Outer Exception: {ex.Message}");

                // Check if there is an inner exception
                if (ex.InnerException != null)
                {
                    // Check if the inner exception is of type SqlException
                    if (ex.InnerException is SqlException sqlException)
                    {
                        // Log the details of the SqlException
                        Console.WriteLine($"SqlException Number: {sqlException.Number}");
                        Console.WriteLine($"SqlException Message: {sqlException.Message}");
                        Console.WriteLine($"SqlException State: {sqlException.State}");
                        Console.WriteLine($"SqlException Class: {sqlException.Class}");
                        Console.WriteLine($"SqlException Procedure: {sqlException.Procedure}");
                        Console.WriteLine($"SqlException LineNumber: {sqlException.LineNumber}");

                        // You can access more properties of the SqlException if needed
                    }
                    else
                    {
                        // Log the details of the inner exception if it's not a SqlException
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }

                   
                }
                return Ok(ex.InnerException.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(Guid id, [FromBody] UpdateJobDto updateJobDto)
        {

            //validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            //Map DTO to Domain model
            var jobDomain = _mapper.Map<Job>(updateJobDto);

            //check if employee exists
           jobDomain = await _jobRepository.UpdateJobsAsync(id,jobDomain);

            if (jobDomain == null)
                return NotFound();


            var jobDto = _mapper.Map<JobDto>(jobDomain);

            return Ok(jobDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            var jobDomain = await _jobRepository.DeleteJobsAsync(id);

            if (jobDomain == null)
                return NotFound();

            return Ok(_mapper.Map<JobDto>(jobDomain));
        }
    }
}

