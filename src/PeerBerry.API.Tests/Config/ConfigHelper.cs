using Microsoft.Extensions.Configuration;

namespace PeerBerry.API.Tests.Config
{
	internal static class ConfigHelper
	{
		public static IConfigurationRoot GetIConfigurationRoot()
		{
			return new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables()
				.Build();
		}

		public static string GetEnvironmentVariable(string name)
		{
			return GetIConfigurationRoot()[name];
		}
	}
}
