using System;
using System.Net;

namespace WeGapApi.Models.Dto
{
	public class SuccessResponseDto
	{
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public LoginResponseDto Result { get; set; }
        public string Message { get; set; }
    }
}

