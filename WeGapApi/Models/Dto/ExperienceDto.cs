using System;
namespace WeGapApi.Models.Dto
{
	public class ExperienceDto
	{
        public Guid Id { get; set; }
        public string CurrentJobTitle { get; set; }
        public string IsWorking { get; set; }
        public string Description { get; set; }
        public DateTime Starting_Date { get; set; }
        public DateTime CompletionDate { get; set; }
        public string CompanyName { get; set; }

        //Link
        public Guid EmployeeId { get; set; }
    }
}

