using System;
using System.ComponentModel.DataAnnotations;

namespace WeGapApi.Models.Dto
{
	public class SignUpRequestDto
	{
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required(ErrorMessage = "FirstName is required")]
		public string FirstName { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string UserName { get; set; }
		public string Role { get; set; }
        [Required(ErrorMessage = "Password is required")]
       // [StringLength(12,MinimumLength =9)] 
        public string Password { get; set; }
		public bool TwoFactorEnabled { get; set; }
    }
}

