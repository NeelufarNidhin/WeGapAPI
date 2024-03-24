using System;
using System.ComponentModel.DataAnnotations;

namespace WeGapApi.Models.Dto
{
	public class AddSkillDto
	{
        [Required]
        public string SkillName { get; set; }
       

        //Link
        public Guid EmployeeId { get; set; }
       
    }
}

