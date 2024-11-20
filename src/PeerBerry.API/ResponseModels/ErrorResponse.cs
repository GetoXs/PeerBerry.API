using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerBerry.API.ResponseModels
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
