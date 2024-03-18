using System;
using System.ComponentModel.DataAnnotations;

namespace WeGapApi.Models.Dto
{
	public class AddJobTypeDto
	{
        [Required(ErrorMessage = "JobType is required")]
        public string JobTypeName { get; set; }
    }
}

