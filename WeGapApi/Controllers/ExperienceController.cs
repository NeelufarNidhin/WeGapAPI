using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            try { 
          var experienceDto =  await _service.ExperienceService.GetAllExperienceAsync();
            return Ok(experienceDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching experience data.");

            }

        }




        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetExperienceById([FromRoute] Guid id)
        {
            try { 
          var experienceDto = await _service.ExperienceService.GetExperienceByIdAsync(id);

            return Ok(experienceDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching experience data.");

            }

        }




        [HttpPost]
        public async Task<IActionResult> AddExperience([FromBody] AddExperienceDto addExperienceDto)

        {
            try { 
           var experienceDto = await _service.ExperienceService.AddExperienceAsync(addExperienceDto);

            return CreatedAtAction(nameof(GetExperienceById), new { id = experienceDto.Id }, experienceDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while creating .");

            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExperience(Guid id, [FromBody] UpdateExperienceDto updateExperienceDto)
        {
            try { 
         var experienceDto =  await _service.ExperienceService.UpdateExperienceAsync(id, updateExperienceDto);

            return Ok(experienceDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while updating experience data.");

            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperience(Guid id)
        {
            try { 
            var experienceDto = await _service.ExperienceService.DeleteExperienceAsync(id);
            return Ok(experienceDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while deleting employer data.");

            }
        }




    }



}

