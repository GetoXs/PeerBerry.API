namespace PeerBerry.API.ResponseModels.LoginResponse
{
	public class LoginResponse
	{
		public string identifier { get; set; }

		public bool? tfa_is_active { get; set; }
		public string tfa_token { get; set; }

		public string access_token { get; set; }
		public int? expires_in { get; set; }
		public string refresh_token { get; set; }
		public string status { get; set; }
		public string clientId { get; set; }
		public bool? phoneConfirmed { get; set; }
		public string verificationStatus { get; set; }
	}
}
