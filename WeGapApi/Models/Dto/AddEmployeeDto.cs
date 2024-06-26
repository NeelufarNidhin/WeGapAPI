﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WeGapApi.Models.Dto
{
	public class AddEmployeeDto
	{


       
        public string ApplicationUserId { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        [Required(ErrorMessage ="Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        [Required(ErrorMessage = "city is required")]
        public string City { get; set; }
        public int Pincode { get; set; }
        public int MobileNumber { get; set; }
        public string Bio { get; set; }
        public string ImageName { get; set; }
        public IFormFile Imagefile { get; set; }


    }
	
}

