using System;
namespace WeGapApi.Models
{
	public class JobType
	{
        public int Id { get; set; }
        public string JobTypeName { get; set; }
        //Link
        public Guid JobId { get; set; }
        public Job Job { get; set; }
    }
}

