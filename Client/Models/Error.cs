using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Models
{
	public class Error
	{
		public string ErrorCode { get; set; }
		public string ErrorStatus { get; set; }
		public string ErrorMessage { get; set; }

		public Error() { }

		public Error(string code,string status,string message) {
			ErrorCode = code;
			ErrorStatus = status;
			ErrorMessage = message;
		}

		public void Error400()
		{
			ErrorCode = "InternalError";
			ErrorStatus = "400";
			ErrorMessage = "Unable to process request: ...";
		}

		public void Error500() {
			ErrorCode = "InternalError";
			ErrorStatus = "500";
			ErrorMessage = "An unexpected error condition was triggered which made impossible to fulfill the request. Please try again or contact support.";
		}
	}
}