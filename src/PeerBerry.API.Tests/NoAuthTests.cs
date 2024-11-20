namespace PeerBerry.API.Tests
{
	public class NoAuthTests
	{
		[Fact]
		public async Task GetGlobal()
		{
			PeerBerryClient client = new PeerBerryClient();
			var result = await client.GetGlobalAsync();
			Assert.NotNull(result);
		}
	}
}