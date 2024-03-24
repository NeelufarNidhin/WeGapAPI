using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeGapApi.Data;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Repository.Interface;
using WeGapApi.Services.Services.Interface;
using WeGapApi.Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = SD.Role_Employee)]
    public class ExperienceController : Controller
    {

        private readonly IServiceManager _service;

        public ExperienceController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
       // [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> GetAllExperiences()
        {

            try { 
          var experienceDto =  await _service.ExperienceService.GetAllExperienceAsync();
            return Ok(experienceDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }

        }




        [HttpGet]
        [Route("{id}")]
        //[Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> GetExperienceById([FromRoute] Guid id)
        {
            try { 
          var experienceDto = await _service.ExperienceService.GetExperienceByIdAsync(id);

            return Ok(experienceDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }

        }

        [HttpGet("employee/{id}")]
       
        //[Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> GetEmployeeExperience( Guid id)
        {
            try
            {
                var experienceDto = await _service.ExperienceService.GetEmployeeExperience(id);

                return Ok(experienceDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }

        }



        [HttpPost]
   
        public async Task<IActionResult> AddExperience([FromBody] AddExperienceDto addExperienceDto)

        {
            try {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var experienceDto = await _service.ExperienceService.AddExperienceAsync(addExperienceDto);

                return CreatedAtAction(nameof(GetExperienceById), new { id = experienceDto.Id }, experienceDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }


        [HttpPut("{id}")]
      //  [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> UpdateExperience(Guid id, [FromBody] UpdateExperienceDto updateExperienceDto)
        {
            try { 
         var experienceDto =  await _service.ExperienceService.UpdateExperienceAsync(id, updateExperienceDto);

            return Ok(experienceDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }

        [HttpDelete("{id}")]
     //   [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> DeleteExperience(Guid id)
        {
            try { 
            var experienceDto = await _service.ExperienceService.DeleteExperienceAsync(id);
            return Ok(experienceDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }




    }



}

