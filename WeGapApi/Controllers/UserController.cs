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
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeGapApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [Authorize (Roles = "admin")]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()

        {
            try
            {
                var users = _userRepository.GetUsers();

                return Ok(users);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError," An error occurred while fetching user data");
            }
           
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateDto updatedUser)
        {
            try { 
            var user = _userRepository.GetUserById(id);

            if (user is null)
                return NotFound();


            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;



            var result = await _userManager.UpdateAsync(user);

            return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while updating user data");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try { 
            var user = await _userManager.FindByIdAsync(id);
            

            if (user is null)
                return NotFound();


            var result = await _userManager.DeleteAsync(user);

            return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while deleting user data");
            }
        }

        [HttpPost("block/{userId}")]
        public async Task<IActionResult> ToggleUserAccountStatus(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    throw new Exception($"User Not Found{userId}");
                }

                if (user != null)
                {
                    user.IsBlocked = !user.IsBlocked;
                    await _userManager.UpdateAsync(user);
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while blocking user");
            }
        
            
          
        }

    }
}

