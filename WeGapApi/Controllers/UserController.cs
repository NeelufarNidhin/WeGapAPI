using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using WeGapApi.Data;
using WeGapApi.Models;
using WeGapApi.Models.Dto;

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
        public Task<IActionResult> GetAll()
        {
            var users = _userRepository.GetUsers();

            return Task.FromResult<IActionResult>(Ok(users));
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateDto updatedUser)
        {
            var user = _userRepository.GetUserById(id);

            if (user is null)
                return NotFound();


            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;



            var result = await _userManager.UpdateAsync(user);

            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            // var logins = user.Logins;
            // var rolesForUser = await _userManager.GetRolesAsync(id);

            if (user is null)
                return NotFound();





            var result = await _userManager.DeleteAsync(user);

            return Ok(result);
        }

        [HttpPost("block/{userId}")]
        public async Task<IActionResult> ToggleUserAccountStatus(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
              
                user.LockoutEnd = DateTime.Now;

            }

            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(1000);
            }

           
            await _userManager.UpdateAsync(user);

            return Ok(user);
          


        }

    }
}

