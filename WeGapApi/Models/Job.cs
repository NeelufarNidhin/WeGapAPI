using System;
using System.ComponentModel.DataAnnotations;

namespace WeGapApi.Models
{
	public class Job
	{
		public Guid Id { get; set; }
        [Required(ErrorMessage = "Title is a required !!")]
        public string JobTitle { get; set; }
		public string Description { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public Guid EmployerId { get; set; }

		//Navigation
		public Employer Employer { get; set; }

    }
}

