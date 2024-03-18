using System;
using System.ComponentModel.DataAnnotations;

namespace WeGapApi.Models.Dto
{
	public class UpdateJobSkillDto
	{
        [Required(ErrorMessage ="JobSkill is required" )]
        public string SkillName { get; set; }

    }
}

