using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeGapApi.Models
{
	public class Employee
	{
        [Key]
        public Guid Id { get; set; }
        
        public string ApplicationUserId { get; set; }
        [Required(ErrorMessage = "DOB is a required !!")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Gender is a required !!")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Address is a required !!")]
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int Pincode { get; set; }
        [Required(ErrorMessage = "Mobile Number is a required !!")]
        public int MobileNumber { get; set; }
        public string Bio { get; set; }
        
        public string ImageName { get; set; }
        public bool CreatedStatus { get; set; } = false;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        //Navigation

        public ApplicationUser ApplicationUser { get; set; }

        [NotMapped]
        public IFormFile Imagefile { get; set; }


    }
}

