using Microsoft.AspNetCore.WebUtilities;
using PeerBerry.API.ResponseModels;
using System.Net;
using System.Net.Http.Json;

namespace PeerBerry.API
{
	public class PeerBerryClient : IDisposable
	{
		public PeerBerryClient()
		{
			var clientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli };
			httpClient = new HttpClient(clientHandler)
			{
				BaseAddress = new Uri("https://api.peerberry.com/"),
			};
		}

		public async Task InitializeAsync(string email, string password, string tfaCode = null)
		{
			this.email = email;
			this.password = password;

			var login = await LoginAsync(email, password);
			if (login.tfa_is_active == true)
			{
				if (tfaCode == null)
					throw new Exception("Authentication need 2FA token");

				var tfaLogin = await Login2FAAsync(login.tfa_token, tfaCode);
				await AuthenticateUsingTokensAsync(tfaLogin.access_token, tfaLogin.refresh_token);
			}
			else
			{
				await AuthenticateUsingTokensAsync(login.access_token, login.refresh_token);
			}
		}

		public async Task AuthenticateUsingTokensAsync(string accessToken, string refreshToken)
		{
			this.accessToken = accessToken;

			var profile = await GetProfileAsync();
			if (profile != null)
				userPublicId = profile.publicId;
		}

		public async Task<GlobalResponse?> GetGlobalAsync()
		{
			return await SendRequest<GlobalResponse>(HttpMethod.Get, $"v1/globals", false);
		}

		public async Task<LoginResponse?> LoginAsync(string email, string password)
		{
			return await SendRequest<LoginResponse>(HttpMethod.Post, $"v1/investor/login", false, new
			{
				email = email,
				password = password,
			});
		}

		public async Task<TFAResponse?> Login2FAAsync(string tfaToken, string tfaCode)
		{
			return await SendRequest<TFAResponse>(HttpMethod.Post, $"v1/investor/login/2fa", false, new
			{
				code = tfaCode,
				tfa_token = tfaToken,
			});
		}

		[Obsolete("Does not work")]
		public async Task<string?> RefreshTokenAsync(string accessToken, string refreshToken)
		{
			return await SendRequest<string>(HttpMethod.Post, $"v1/investor/refresh-token", false,
				new
				{
					access_token = accessToken,
					refresh_token = refreshToken,
				});
		}

		public async Task<ProfileResponse?> GetProfileAsync()
		{
			return await SendRequest<ProfileResponse>(HttpMethod.Get, $"v2/investor/profile", true);
		}

		public async Task<LoansResponse?> GetLoansAsync(int offset = 0, int pageSize = 40, Dictionary<string, string?>? searchParams = null, string sort = "-loanId")
		{
			ValidateAuth();

			var url = $"v1/{userPublicId}/loans?sort={sort}&offset={offset}&pageSize={pageSize}";
			if (searchParams != null)
				url = QueryHelpers.AddQueryString(url, searchParams);

			return await SendRequest<LoansResponse>(HttpMethod.Get, url, true);
		}

		private void ValidateAuth()
		{
			if (!IsAuth())
				throw new Exception($"Authentication not inicialized.");
		}

		async Task<T?> SendRequest<T>(HttpMethod method, string url, bool isAuthorized, object? body = null)
		{
			HttpRequestMessage request = new HttpRequestMessage(method, url)
			{
				Headers = {
					Authorization = isAuthorized ? new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken) : null,
				},
				Content = body != null ? JsonContent.Create(body) : null,
			};
			request.Headers.Add("Accept", "application/json; charset=UTF-8");
			request.Headers.Add("Origin", "https://peerberry.com");
			request.Headers.Add("Referer", "https://peerberry.com/en/client/overview");
			request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36");

			var response = await httpClient.SendAsync(request);
			if (response.StatusCode != HttpStatusCode.OK)
			{
				if (response.StatusCode == HttpStatusCode.Unauthorized)
					throw new UnauthorizedAccessException(await response.Content.ReadAsStringAsync());
				//TODO: log
				throw new Exception(await response.Content.ReadAsStringAsync());
			}
			return await response.Content.ReadFromJsonAsync<T>();
		}

		private string? email;
		private string? password;
		private string? accessToken;
		private string? refreshToken;
		private string? userPublicId;
		private readonly HttpClient httpClient;

		private bool IsAuth() => userPublicId != null;

		public void Dispose()
		{
			((IDisposable)httpClient).Dispose();
		}
	}
}
