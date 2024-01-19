using System;
using System.ComponentModel.DataAnnotations;

namespace WeGapApi.Models
{
	public class Employee
	{
        [Key]
        public Guid Id { get; set; }
        
        public string ApplicationUserId { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int Pincode { get; set; }
        public int MobileNumber { get; set; }
        //Navigation

        public ApplicationUser ApplicationUser { get; set; }


    }
}

