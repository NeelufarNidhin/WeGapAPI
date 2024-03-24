using System;
namespace WeGapApi.Models.Dto
{
	public class LoginResponseDto
	{
		public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}

