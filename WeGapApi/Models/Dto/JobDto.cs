using System;
using System.ComponentModel.DataAnnotations;

namespace WeGapApi.Models.Dto
{
	public class JobDto
	{
        public Guid Id { get; set; }
        [Required]
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public Guid EmployerId { get; set; }
        public string Experience { get; set; }
        public double Salary { get; set; }
        public  List<JobSkill>  JobSkills{ get; set; }
        public List<JobType> JobTypes { get; set; }

        public Employer Employer { get; set; }

    }
}

