using System;
namespace WeGapApi.Models.Dto
{
	public class AddJobDto
	{

        public string JobTitle { get; set; }
        public string Description { get; set; }
        public string Experience { get; set; }
        public double Salary { get; set; }
        public Guid EmployerId { get; set; }
        public List<AddJobSkillDto> JobSkills { get; set; }
        public List<AddJobTypeDto> JobTypes { get; set; }
    }
}

