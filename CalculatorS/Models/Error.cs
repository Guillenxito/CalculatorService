namespace CalculatorS.Models
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

		public void Error400(string message)
		{
			ErrorCode = "InternalError";
			ErrorStatus = "400";
			ErrorMessage = message;
		}

		public void Error500(string message) {
			ErrorCode = "InternalError";
			ErrorStatus = "500";
			ErrorMessage = message;
		}
	}
}