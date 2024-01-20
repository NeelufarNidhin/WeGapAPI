using System;
namespace WeGapApi.Models
{
	public class Job
	{
		public Guid Id { get; set; }
		public string JobTitle { get; set; }
		public string Description { get; set; }
		public Guid EmployerId { get; set; }

		//Navigation
		public Employer Employer { get; set; }

    }
}

