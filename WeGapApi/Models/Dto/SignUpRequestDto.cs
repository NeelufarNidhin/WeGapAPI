using System;
namespace WeGapApi.Models.Dto
{
	public class SignUpRequestDto
	{
		
		public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
		public string Role { get; set; }
		public string Password { get; set; }
		public bool TwoFactorEnabled { get; set; }
    }
}

