using System;
using System.Net;

namespace WeGapApi.Models.Dto
{
	public class ErrorResponseDto
	{
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}

