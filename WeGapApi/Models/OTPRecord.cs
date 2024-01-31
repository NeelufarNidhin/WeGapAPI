using System;
using System.ComponentModel.DataAnnotations;

namespace WeGapApi.Models
{
	public class OTPRecord
	{
		[Key]
      public  int Id { get; set; }
	  public string	UserId { get; set; }
      public string  Otp { get; set; }
       public DateTime TimeStamp { get; set; } = DateTime.UtcNow;


		public ApplicationUser ApplicationUser { get; set; }
	}
}

