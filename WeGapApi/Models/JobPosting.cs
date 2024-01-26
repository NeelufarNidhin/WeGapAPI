using System;
namespace WeGapApi.Models
{
	public class JobPosting
	{
        public Guid Id { get; set; }
        public DateTime PostedDate { get; set; }

        //Link
        public Guid JobId { get; set; }
        public Job Job { get; set; }
    }
}

