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
    public class EducationController : Controller
    {
        public readonly IEducationRepository _educationRepository;
        public IMapper _mapper;
        public EducationController(IEducationRepository educationRepository, IMapper mapper)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEducation()
        {
            var education = await _educationRepository.GetAllAsync();
            var educationDto = _mapper.Map<List<EducationDto>>(education);

            return Ok(educationDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEducationById([FromRoute] Guid id)
        {
            var education = await _educationRepository.GetEducationByIdAsync(id);

            var educationDto = _mapper.Map<EducationDto>(education);

            if (education is null)
                return NotFound();

            return Ok(educationDto);
        }


        [HttpPost]
        public async Task<IActionResult> AddEducation([FromBody] AddEducationDto addEducationDto) 
        {

          if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var educationDomain = _mapper.Map<Education>(addEducationDto);
            await _educationRepository.AddEducationAsync(educationDomain);
            var educationDto = _mapper.Map<EducationDto>(educationDomain);
            return CreatedAtAction(nameof(GetEducationById), new { id = educationDto.Id }, educationDto);
        }

        [HttpPut("{id}")]

        public async Task <IActionResult> UpdateEducation(Guid id , [FromBody] UpdateEducationDto updateEducationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var educationDomain = _mapper.Map<Education>(updateEducationDto);

            await _educationRepository.UpdateEducationAsync( id,educationDomain);

            var educationDto = _mapper.Map<EducationDto>(educationDomain);

            return Ok(educationDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducation(Guid id)
        {
            var educationDomain = await _educationRepository.DeleteEducationAsync(id);

            if (educationDomain == null)
                return NotFound();

            return Ok(_mapper.Map<EducationDto>(educationDomain));
        }

    }
}

