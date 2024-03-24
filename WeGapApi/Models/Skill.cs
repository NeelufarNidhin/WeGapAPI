using System;
namespace WeGapApi.Models
{
	public class Skill
	{
        public Guid Id { get; set; }
        public string SkillName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Link
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}

