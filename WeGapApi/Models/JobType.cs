﻿using System;
namespace WeGapApi.Models
{
	public class JobType
	{
        public int Id { get; set; }
        public string JobTypeName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

