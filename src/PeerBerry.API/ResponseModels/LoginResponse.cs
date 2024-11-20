using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerBerry.API.ResponseModels
{

	public class LoginResponse
	{
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
