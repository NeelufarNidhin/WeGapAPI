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
using WeGapApi.Services.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    public class ExperienceController : Controller
    {

        private readonly IServiceManager _service;

        public ExperienceController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExperiences()
        {

          var experienceDto =  await _service.ExperienceService.GetAllExperienceAsync();
            return Ok(experienceDto);

        }




        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetExperienceById([FromRoute] Guid id)
        {
          var experienceDto = await _service.ExperienceService.GetExperienceByIdAsync(id);

            return Ok(experienceDto);

        }




        [HttpPost]
        public async Task<IActionResult> AddExperience([FromBody] AddExperienceDto addExperienceDto)

        { 

           var experienceDto = await _service.ExperienceService.AddExperienceAsync(addExperienceDto);

            return CreatedAtAction(nameof(GetExperienceById), new { id = experienceDto.Id }, experienceDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExperience(Guid id, [FromBody] UpdateExperienceDto updateExperienceDto)
        {

         var experienceDto =  await _service.ExperienceService.UpdateExperienceAsync(id, updateExperienceDto);

            return Ok(experienceDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperience(Guid id)
        {
            var experienceDto = await _service.ExperienceService.DeleteExperienceAsync(id);
            return Ok(experienceDto);
        }




    }



}

