using System;
namespace WeGapApi.Models.Dto
{
	public class AddJobDto
	{

        public string JobTitle { get; set; }
        public string Description { get; set; }
        public Guid EmployerId { get; set; }
    }
}

