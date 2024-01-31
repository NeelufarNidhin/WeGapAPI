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


        [HttpGet]
        public async Task<IActionResult> GetAllEmployers()
        {

            var employers = await _employerRepository.GetAllEmployerAsync();

            var employerDto = _mapper.Map<List<EmployerDto>>(employers);

            return Ok(employerDto);

        }




        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEmployerById([FromRoute] Guid id)
        {
            //get data from Database
            var employer = await _employerRepository.GetEmployerByIdAsync(id);


            //return DTO usimg Mapper
            var employerDto = _mapper.Map<EmployerDto>(employer);

            if (employer is null)
                return NotFound();

            return Ok(employerDto);

        }



       
        [HttpPost]
        public async Task<IActionResult> AddEmployer([FromBody] AddEmployerDto addemployerDto)

        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employerDomain = _mapper.Map<Employer>(addemployerDto);
          //  var userDomain = _employerRepository.GetEmployerByIdAsync;

            var EmployerDomain = await _employerRepository.AddEmployerAsync(employerDomain);

            var employerDto = _mapper.Map<EmployerDto>(EmployerDomain);



            return CreatedAtAction(nameof(GetEmployerById), new { id = employerDto.Id }, employerDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployer(Guid id, [FromBody] UpdateEmployerDto updateEmployerDto)
        {

            //validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            //Map DTO to Domain model
            var employerDomain = _mapper.Map<Employer>(updateEmployerDto);

            //check if employee exists
            employerDomain = await _employerRepository.UpdateEmployerAsync(id, employerDomain);

            if (employerDomain == null)
                return NotFound();


            var employerDto = _mapper.Map<EmployerDto>(employerDomain);

            return Ok(employerDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployer(Guid id)
        {
            var employerDomain = await _employerRepository.DeleteEmployerAsync(id);

            if (employerDomain == null)
                return NotFound();

            return Ok(_mapper.Map<EmployerDto>(employerDomain));
        }


    }
}

