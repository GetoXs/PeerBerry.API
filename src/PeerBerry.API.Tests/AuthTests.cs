using Microsoft.Extensions.Configuration;
using PeerBerry.API.Tests.Config;

namespace PeerBerry.API.Tests
{
	public class AuthTests
	{
		private PeerBerryClient client;

		public AuthTests()
		{
			client = new PeerBerryClient();

		}

		private async Task Init()
		{
			await client.InitializeAsync(ConfigHelper.GetIConfigurationRoot()["PeerBerryEmail"], ConfigHelper.GetIConfigurationRoot()["PeerBerryPassword"]);
		}

		[Fact]
		private async Task RefreshToken()
		{
			await Init();
		}

		[Fact]
		public async Task GetLoans()
		{
			await Init();
			var result = await client.GetLoansAsync(searchParams: new Dictionary<string, string?>
			{
				{ "groupGuarantee", "1" },
				{ "hideInvested", "1" },
			});
			Assert.NotNull(result?.data);
		}
	}
}