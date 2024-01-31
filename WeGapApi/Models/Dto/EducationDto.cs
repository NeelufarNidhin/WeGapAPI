using System;
namespace WeGapApi.Models.Dto
{
	public class EducationDto
	{
        public Guid Id { get; set; }
        public string Degree { get; set; }
        public string Subject { get; set; }
        public string University { get; set; }
        public double Percentage { get; set; }
        public DateTime Starting_Date { get; set; }
        public DateTime CompletionDate { get; set; }

        //Link
        public Guid EmployeeId { get; set; }
    }
}

