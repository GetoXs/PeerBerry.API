namespace PeerBerry.API.Initialize
{
	public class InitResult
	{
		public InitResultEnum Result { get; set; }
		public string Token { get; set; }
	}
	public enum InitResultEnum
	{
		Succeed,
		EmailCodeNeeded,
		TwoFaNeeded,
	}
}
