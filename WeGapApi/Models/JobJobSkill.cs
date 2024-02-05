using System;
namespace WeGapApi.Models
{
	public class JobJobSkill
	{
		public Guid Id { get; set; }
		public Guid JobId { get; set; }
        public Job Job { get; set; }

        public Guid JobSkillId { get; set; }
        public JobSkill JobSkill { get; set; }
    }
}

