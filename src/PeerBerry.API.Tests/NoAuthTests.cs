using Microsoft.Extensions.Logging;
using PeerBerry.API.Tests.Config;

namespace PeerBerry.API.Tests
{
	public class NoAuthTests
	{
		private PeerBerryClient client;

		public NoAuthTests()
		{
			ILoggerFactory loggerFactory = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();
			client = new PeerBerryClient(loggerFactory.CreateLogger<PeerBerryClient>());
		}

		[Fact]
		[Trait("CI", "true")]
		public async Task GetGlobal()
		{
			var result = await client.GetGlobalAsync();
			Assert.NotNull(result);
		}

		[Fact]
		public async Task RefreshToken()
		{
			var result = await client.RefreshTokenAsync(ConfigHelper.GetEnvironmentVariable("PeerBerryAccessToken"),
				ConfigHelper.GetEnvironmentVariable("PeerBerryRefreshToken"));
			Assert.NotNull(result);
		}
	}
}