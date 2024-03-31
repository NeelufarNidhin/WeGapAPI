using System;
using System.ComponentModel.DataAnnotations;

namespace WeGapApi.Models
{
	public class Job
	{
        public Guid Id { get; set; }
        [Required(ErrorMessage ="JobTitle is required")]
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public string Experience { get; set; }
        public double Salary { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Navigation property
        public Guid EmployerId { get; set; }
        public Employer Employer { get; set; }

        [Required(ErrorMessage = "JobType is required")]
        public Guid JobTypeId { get; set; }
        public JobType JobType { get; set; }

        [Required(ErrorMessage = "Jobskill is required")]
        public ICollection<JobJobSkill> JobJobSkill { get; set; }
        public JobSkill JobSkill { get; set; }

    }
}

