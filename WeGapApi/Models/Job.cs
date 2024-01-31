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
        public Guid EmployerId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<JobSkill> JobSkills { get; set; }
        public List<JobType> JobTypes { get; set; }

        public Employer Employer { get; set; }
       // public JobType JobType { get; set; }
       // public JobSkill JobSkill { get; set; }

    }
}

