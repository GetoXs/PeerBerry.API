﻿namespace PeerBerry.API.ResponseModels.TFAResponse
{
	public class TFAResponse
	{
		public string access_token { get; set; }
		public int expires_in { get; set; }
		public string refresh_token { get; set; }
		public string status { get; set; }
	}

}
