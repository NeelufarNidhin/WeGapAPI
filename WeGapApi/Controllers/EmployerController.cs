using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeGapApi.Data;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    public class EmployerController : ControllerBase
    {

        private readonly IEmployerRepository _employerRepository;
        private readonly IMapper _mapper;


        public EmployerController(IEmployerRepository employerRepository, IMapper mapper)

        {

            _employerRepository = employerRepository;
            _mapper = mapper;
        }


        //public async Task<IActionResult> GetAll()
        //{

        //    var employees = _employerRepository
        //}



        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployerById(Guid id)
        {
            var employer = await _employerRepository.GetEmployerByIdAsync(id);

            if (employer is null)
                return NotFound();

            return Ok(employer);

        }

        

        [HttpPost]
        public async Task<IActionResult> AddEmployer([FromBody] EmployerDto employerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var newEmployerId = await _employerRepository.AddEmployerAsync(employerDto);
            return CreatedAtAction(nameof(GetEmployerById), new { id = newEmployerId }, null);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployer(Guid id, [FromBody] EmployerDto employerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isUpdated = await _employerRepository.UpdateEmployerAsync(id, employerDto);

            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployer(Guid id)
        {
            var isDeleted = await _employerRepository.DeleteEmployerAsync(id);

            if (!isDeleted)
                return NotFound();

            return NoContent();
        }

        
    }
}

