namespace PeerBerry.API.ResponseModels.RefreshTokenResponse
{
	public class RefreshTokenResponse
	{
		public string access_token { get; set; }
		public int expires_in { get; set; }
		public string refresh_token { get; set; }
	}

}
