using System;
namespace WeGapApi.Models.Dto
{
    public class UpdateEmployeeDto
    {
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int Pincode { get; set; }
        public int MobileNumber { get; set; }
        public IFormFile Imagefile { get; set; }
    }

}