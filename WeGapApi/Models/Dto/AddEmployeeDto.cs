using System;
namespace WeGapApi.Models.Dto
{
	public class AddEmployeeDto
	{


       
        public string ApplicationUserId { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int Pincode { get; set; }
        public int MobileNumber { get; set; }



    }
	
}

