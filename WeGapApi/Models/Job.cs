using System;
namespace WeGapApi.Models
{
	public class Job
	{
        public Guid Id { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public string Experience { get; set; }
        public double Salary { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Navigation property
        public Guid EmployerId { get; set; }
        public Employer Employer { get; set; }

        public Guid JobTypeId { get; set; }
  

        public List<JobJobSkill> JobJobSkill { get; set; }

    }
}

