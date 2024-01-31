using System;
namespace WeGapApi.Models
{
	public class JobSkill
	{
        public int Id { get; set; }
        public string SkillName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

