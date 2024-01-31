using System;
namespace WeGapApi.Models.Dto
{
	public class UpdateExperienceDto
	{
        public string CurrentJobTitle { get; set; }
        public string IsWorking { get; set; }
        public string Description { get; set; }
        public DateTime Starting_Date { get; set; }
        public DateTime CompletionDate { get; set; }
        public string CompanyName { get; set; }

    }
}

