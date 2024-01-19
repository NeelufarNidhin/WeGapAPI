﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WeGapApi.Models
{
	public class ApplicationUser : IdentityUser
	{
        [Required(ErrorMessage = "First Name is a required !!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is a required !!")]
        public string LastName { get; set; }
        public string Role { get; set; }
        
		public DateTime CreateAt { get; set; }
		public string Createby { get; set; }

    }
}
