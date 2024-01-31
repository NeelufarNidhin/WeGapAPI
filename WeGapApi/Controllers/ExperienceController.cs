using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeGapApi.Data;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Repository.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    public class ExperienceController : Controller
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IMapper _mapper;


        public ExperienceController(IExperienceRepository experienceRepository , IMapper mapper)
        {
            _experienceRepository = experienceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExperiences()
        {

            var experience = await _experienceRepository.GetAllExperienceAsync();
           var experienceDto = _mapper.Map<List<ExperienceDto>>(experience);
            return Ok(experienceDto);

        }




        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetExperienceById([FromRoute] Guid id)
        {
            //get data from Database
            var experience = await _experienceRepository.GetExperienceByIdAsync(id);


            //return DTO usimg Mapper
            var experienceDto = _mapper.Map<ExperienceDto>(experience);

            if (experience is null)
                return NotFound();

            return Ok(experienceDto);

        }




        [HttpPost]
        public async Task<IActionResult> AddExperience([FromBody] AddExperienceDto addExperienceDto)

        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var experienceDomain = _mapper.Map<Experience>(addExperienceDto);
            //var userDomain = _experienceRepository.GetExperienceByIdAsync;

            var ExperienceDomain = await _experienceRepository.AddExperienceAsync(experienceDomain);

            var experienceDto = _mapper.Map<ExperienceDto>(ExperienceDomain);



            return CreatedAtAction(nameof(GetExperienceById), new { id = experienceDto.Id }, experienceDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExperience(Guid id, [FromBody] UpdateExperienceDto updateExperienceDto)
        {

            //validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            //Map DTO to Domain model
            var experienceDomain = _mapper.Map<Experience>(updateExperienceDto);

            //check if experience exists
            experienceDomain = await _experienceRepository.UpdateExperienceAsync(id, experienceDomain);

            if (experienceDomain == null)
                return NotFound();


            var experienceDto = _mapper.Map<ExperienceDto>(experienceDomain);

            return Ok(experienceDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperience(Guid id)
        {
            var experinceDomain = await _experienceRepository.DeleteExperienceAsync(id);

            if (experinceDomain == null)
                return NotFound();

            return Ok(_mapper.Map<ExperienceDto>(experinceDomain));
        }




    }



}

