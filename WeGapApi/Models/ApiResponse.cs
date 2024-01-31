using System;
using System.Net;

namespace WeGapApi.Models
{
	public class ApiResponse
	{


		//public ApiResponse()
		//{
		//	ErrrorMessages = new List<string>();
		//}

		public HttpStatusCode StatusCode { get; set; }
		public bool IsSuccess { get; set; } = true;
		public string Message { get; set; }
		public string ErrorMessages { get; set; }
		public object Result { get; set; }
    }
}

