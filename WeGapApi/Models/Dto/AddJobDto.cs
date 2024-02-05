using System;
namespace WeGapApi.Models.Dto
{
	public class AddJobDto
	{

        public string JobTitle { get; set; }
        public string Description { get; set; }
        public string Experience { get; set; }
        public double Salary { get; set; }
       

        //Navigation property
        public Guid EmployerId { get; set; }
    

        public Guid JobTypeId { get; set; }


        public List<JobJobSkill> JobJobSkill { get; set; }
    }
}

