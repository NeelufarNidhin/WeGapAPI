using System;
using System.ComponentModel.DataAnnotations;

namespace WeGapApi.Models.Dto
{
	public class AddEmployerDto
	{
       
        public string CompanyName { get; set; }
        public string ApplicationUserId { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
    }
}

