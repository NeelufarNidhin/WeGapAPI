using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using WeGapApi.Models.Dto;
using WeGapApi.Services.Services.Interface;
using WeGapApi.Utility;

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = SD.Role_Employee)]
    public class SkillController : Controller
    {
        private readonly IServiceManager _service;

        public SkillController(IServiceManager service)
        {
            _service = service;

        }

        [HttpGet]
        [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> GetAllSkill()

        {
            try
            {
                var skillDto = await _service.SkillService.GetAllSkillAsync();

                return Ok(skillDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }



        }

        [HttpGet("employee/{id}")]
        [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> GetEmployeeSkill(Guid id)

        {
            try
            {
                var skillDto = await _service.SkillService.GetEmployeeSkillAsync(id);

                return Ok(skillDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }



        }


        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> GetSkillById([FromRoute] Guid id)
        {

            try
            {
                var skillDto = await _service.SkillService.GetSkillByIdAsync(id);
                return Ok(skillDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        [HttpPost]
        [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> AddSkill([FromBody] AddSkillDto addSkillDto)
        {

            try
            {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var skillDto = await _service.SkillService.AddSkillAsync(addSkillDto);
                return CreatedAtAction(nameof(GetSkillById), new { id = skillDto.Id }, skillDto);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> UpdateSkillJob(Guid id, [FromBody] UpdateSkillDto updateSkillDto)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                else
                {
                    var skillDto = await _service.SkillService.UpdateSkillAsync(id, updateSkillDto);
                    return Ok(skillDto);
                }


            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> DeleteSkill(Guid id)
        {
            try
            {
                await _service.SkillService.DeleteSkillAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}



