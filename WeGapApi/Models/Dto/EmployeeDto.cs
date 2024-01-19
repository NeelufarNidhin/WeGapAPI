using System;
namespace WeGapApi.Models.Dto
{
	public class EmployeeDto
	{
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int Pincode { get; set; }
        public int MobileNumber { get; set; }
        public DateTime DOB { get; set; }
        public string ApplicationUserId { get; set; }


      //  public UserDto UserDto { get; set; }
         public ApplicationUser ApplicationUser { get; set; }
    }
}

