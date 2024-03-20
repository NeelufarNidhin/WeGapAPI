using System;
using System.ComponentModel.DataAnnotations;

namespace WeGapApi.Models
{
	public class Experience
	{
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Job Title is Required")]
        public string CurrentJobTitle { get; set; }
        [Required(ErrorMessage = "Working Status is Required")]
        public string IsWorking { get; set; }
        [Required(ErrorMessage = "Company Name is Required")]
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public DateTime Starting_Date { get; set; }
        public DateTime CompletionDate { get; set; }
      

        //Link
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}

