using System;
namespace WeGapApi.Models.Dto
{
	public class SkillDto
	{
        public Guid Id { get; set; }
        public string SkillName { get; set; }
       

        //Link
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}

