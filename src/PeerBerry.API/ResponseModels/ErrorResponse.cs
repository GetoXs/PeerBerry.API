namespace PeerBerry.API.ResponseModels.ErrorResponse
{
	public class ErrorResponse
	{
		public string message { get; set; }
		public Error[] errors { get; set; }
	}
	public class Error
	{
		public string message { get; set; }
		public int code { get; set; }
	}
}
