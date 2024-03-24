using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
        

        public UserController(IServiceManager service)
        {
            _service = service;
        }

        
        [HttpGet()]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> GetAllUser()

        {
            try
            {
                var users = _service.UserService.GetAllUsers();

                return Ok(users);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
           
        }

       
        [HttpPut("{id}")]
       [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserDto updatedUser)
        {
            try
            {
               
               var userDto = await _service.UserService.UpdateUser(id,updatedUser);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

            
        }

      
        [HttpDelete("{id}")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> Delete(string id)
        {
            try {
                var userDto = await _service.UserService.DeleteUser(id);

                 return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("block/{userId}")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> ToggleUserAccountStatus(string userId)
        {
            try
            {
                var user = await _service.UserService.BlockUnblock(userId);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        
            
          
        }

    }
}

