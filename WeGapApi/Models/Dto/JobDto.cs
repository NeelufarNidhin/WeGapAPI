using System;
namespace WeGapApi.Models.Dto
{
	public class JobDto
	{
        public Guid Id { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public Guid EmployerId { get; set; }

        public Employer Employer { get; set; }

    }
}

