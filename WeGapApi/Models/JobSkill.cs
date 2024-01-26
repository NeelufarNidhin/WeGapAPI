using System;
namespace WeGapApi.Models
{
	public class JobSkill
	{
        public int Id { get; set; }
        public string SkillName { get; set; }


        //Link
        public Guid JobId { get; set; }
        public Job Job { get; set; }
    }
}

