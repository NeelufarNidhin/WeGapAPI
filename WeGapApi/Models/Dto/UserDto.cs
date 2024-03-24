using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WeGapApi.Models.Dto
{
	public class UserDto : IdentityUser
	{


        [Required(ErrorMessage = "First Name is a required !!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is a required !!")]
        public string LastName { get; set; }
        public string Role { get; set; }

       
        public bool IsBlocked { get; set; }
        public bool IsProfile { get; set; }

        public ApplicationUser applicationUser { get; set; }

    }
}

