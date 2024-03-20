using System;
using System.ComponentModel.DataAnnotations;

namespace WeGapApi.Models.Dto
{
	public class UpdateEducationDto
	{

        [Required(ErrorMessage = "Degree is Required")]
        public string Degree { get; set; }
        [Required(ErrorMessage = "Subject is Required")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "University is Required")]
        public string University { get; set; }
        public double Percentage { get; set; }
        public DateTime Starting_Date { get; set; }
        public DateTime CompletionDate { get; set; }

    
    }
}

