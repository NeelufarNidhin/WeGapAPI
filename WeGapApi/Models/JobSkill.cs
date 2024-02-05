using System;
namespace WeGapApi.Models
{
	public class JobSkill
	{
        public Guid Id { get; set; }
        public string SkillName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<JobJobSkill> JobJobSkill { get; set; }
    }
}

