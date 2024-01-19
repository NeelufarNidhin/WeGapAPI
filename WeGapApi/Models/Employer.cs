using System;
using System.ComponentModel.DataAnnotations;

namespace WeGapApi.Models
{
	public class Employer 
	{
		[Key]
		public Guid Id { get; set; }
		[Required (ErrorMessage = "Company Name is a required !!")]
        public string CompanyName { get; set; }
        public string ApplicationUserId { get; set; }
        public string Location  { get; set; }
        public string Website { get; set; }
		public string Description { get; set; }
		public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        //Navigation

        public ApplicationUser ApplicationUser { get; set; }

    }
}

