using System;
using System.ComponentModel.DataAnnotations;

namespace WeGapApi.Models.Dto
{
	public class UpdateSkillDto
	{
        [Required]
        public string SkillName { get; set; }


    }
}

