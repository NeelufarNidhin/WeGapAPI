using System;
using System.ComponentModel.DataAnnotations;

namespace WeGapApi.Models
{
	public class Education
	{
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Degree is Required")]
        public string Degree { get; set; }
        [Required(ErrorMessage = "Subject is Required")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "University is Required")]
        public string University { get; set; }
        public double Percentage { get; set; }
        public DateTime Starting_Date { get; set; }
        public DateTime CompletionDate { get; set; }

        
        public Guid EmployeeId { get; set; }
       public Employee Employee { get; set; }
    }
}

