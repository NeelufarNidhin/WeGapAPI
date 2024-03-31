using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using WeGapApi.Data;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Services.Services.Interface;
using WeGapApi.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {

        private readonly IServiceManager _service;

        private readonly ApiResponse _response;
        public UserController(IServiceManager service)
        {
            _service = service;
            _response = new ApiResponse();
        }

        
        [HttpGet()]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> GetAllUser([FromQuery] string searchString, [FromQuery] string role,
            [FromQuery] int pageNumber = 1 , [FromQuery] int pageSize = 10)

        {
            try
            {
                // var users = _service.UserService.GetAllUsers();
                List<UserDto> users;
                if (string.IsNullOrEmpty(searchString))
                {
                    users = await _service.UserService.GetAllUsers(pageNumber, pageSize);
                    if (!string.IsNullOrEmpty(role))
                    {
                        users = users.Where(u => u.Role == role).ToList();
                    }
                }
               
                else
                {
                    var searchResult = await _service.UserService.GetSearchQuery(searchString);
                     users = searchResult.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
               

               
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = users;
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

       
        [HttpPut("{id}")]
       [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserDto updatedUser)
        {
            try
            {
               
               var userDto = await _service.UserService.UpdateUser(id,updatedUser);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = userDto;
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

      
        [HttpDelete("{id}")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> Delete(string id)
        {
            try {
                var userDto = await _service.UserService.DeleteUser(id);

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = userDto;
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

        [HttpPost("block/{userId}")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> ToggleUserAccountStatus(string userId)
        {
            try
            {
                var user = await _service.UserService.BlockUnblock(userId);

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = user;
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

    }
}

