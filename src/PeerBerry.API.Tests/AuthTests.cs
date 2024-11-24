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
			if (ConfigHelper.GetIConfigurationRoot()["PeerBerryAccessToken"] != null
				&& ConfigHelper.GetIConfigurationRoot()["PeerBerryRefreshToken"] != null)
				await client.InitializeUsingTokensAsync(ConfigHelper.GetEnvironmentVariable("PeerBerryAccessToken"), ConfigHelper.GetEnvironmentVariable("PeerBerryRefreshToken"));
			else
				await client.InitializeUsingEmailAsync(ConfigHelper.GetEnvironmentVariable("PeerBerryEmail"), ConfigHelper.GetEnvironmentVariable("PeerBerryPassword"));
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

		[Fact]
		public async Task GetInvestments()
		{
			await Init();
			var result = await client.GetInvestmentsAsync(searchParams: new Dictionary<string, string?>
			{
				{ "type", "CURRENT" },
			});
			Assert.NotNull(result?.data);
		}

		[Fact]
		public async Task GetBalanceMain()
		{
			await Init();
			var result = await client.GetBalanceMainAsync();
			Assert.True(result?.totalBalance > 0);
		}

		[Fact]
		public async Task GetProfile()
		{
			await Init();
			var result = await client.GetProfileAsync();
			Assert.NotNull(result?.publicId);
		}
	}
}