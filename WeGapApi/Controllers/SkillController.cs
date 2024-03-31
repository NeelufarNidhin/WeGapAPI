using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using WeGapApi.Models.Dto;
using WeGapApi.Services.Services.Interface;
using WeGapApi.Utility;
using WeGapApi.Models;

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = SD.Role_Employee)]
    public class SkillController : Controller
    {
        private readonly IServiceManager _service;
        private readonly ApiResponse _response;
        public SkillController(IServiceManager service)
        {
            _service = service;
            _response = new ApiResponse();

        }

        [HttpGet]
        [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> GetAllSkill()

        {
            try
            {
                var skillDto = await _service.SkillService.GetAllSkillAsync();
                _response.Result = skillDto;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }



        }

        [HttpGet("employee/{id}")]
        [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> GetEmployeeSkill(Guid id)

        {
            try
            {
               
                var skillDto = await _service.SkillService.GetEmployeeSkillAsync(id);
                _response.Result = skillDto;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
                //return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
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
                _response.Result = skillDto;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);

                //return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        [HttpPost]
        [Authorize(Roles = SD.Role_Employee)]
        public async Task<ActionResult<ApiResponse>> AddSkill([FromBody] AddSkillDto addSkillDto)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string>() { ModelState.ToString() };
                    return BadRequest(_response);
                }
                else
                {
                    var skillDto = await _service.SkillService.AddSkillAsync(addSkillDto);
                    _response.Result = skillDto;
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtAction(nameof(GetSkillById), new { id = skillDto.Id }, _response);
                }   
               

            }
            catch (InvalidOperationException ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
                // return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = SD.Role_Employee)]
        public async Task<ActionResult<ApiResponse>> UpdateSkillJob(Guid id, [FromBody] UpdateSkillDto updateSkillDto)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                   
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string>() { ModelState.ToString() };
                    return BadRequest(_response);
                }
                    


                else
                {
                    var skillDto = await _service.SkillService.UpdateSkillAsync(id, updateSkillDto);
                    _response.Result = skillDto;
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }


            }
            catch (InvalidOperationException ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
                // return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = SD.Role_Employee)]
        public async Task<IActionResult> DeleteSkill(Guid id)
        {
            try
            {
                var skillDto =await _service.SkillService.DeleteSkillAsync(id);
                _response.Result = skillDto;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response); ;
            }
            catch (InvalidOperationException ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
                // return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}



