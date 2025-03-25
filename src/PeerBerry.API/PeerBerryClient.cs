using Microsoft.AspNetCore.WebUtilities;
using PeerBerry.API.Initialize;
using PeerBerry.API.ResponseModels.BalanceMainReponse;
using PeerBerry.API.ResponseModels.EmailCodeResponse;
using PeerBerry.API.ResponseModels.GlobalResponse;
using PeerBerry.API.ResponseModels.InvestmentsResponse;
using PeerBerry.API.ResponseModels.LoansResponse;
using PeerBerry.API.ResponseModels.LoginResponse;
using PeerBerry.API.ResponseModels.PostLoanResponse;
using PeerBerry.API.ResponseModels.ProfileResponse;
using PeerBerry.API.ResponseModels.RefreshTokenResponse;
using PeerBerry.API.ResponseModels.TFAResponse;
using System.Reflection.Metadata.Ecma335;

namespace PeerBerry.API
{
	public partial class PeerBerryClient : IDisposable
	{
		public PeerBerryClient()
		{
			_peerBerryProxyApi = new PeerBerryProxyApi();
		}
		public PeerBerryClient(PeerBerryProxyApi peerBerryProxyApi)
		{
			_peerBerryProxyApi = peerBerryProxyApi;
		}

		#region Initialize
		public async Task<InitResult> InitializeUsingEmailAsync(string email, string password)
		{
			_email = email;
			_password = password;

			var login = await LoginAsync(email, password);
			if (login.tfa_is_active == true)
			{
				return new InitResult
				{
					Result = InitResultEnum.TwoFaNeeded,
					Token = login.tfa_token,
				};
			}
			else if(login.identifier != null)
			{
				return new InitResult
				{
					Result = InitResultEnum.EmailCodeNeeded,
					Token = login.identifier,
				};
			}
			else
			{
				await AuthenticateAsync(login.access_token, login.refresh_token);
				return new InitResult
				{
					Result = InitResultEnum.Succeed,
				};
			}
		}
		public async Task InitializeUsing2Fa(string tfaToken, string tfaCode)
		{
			var tfaLogin = await Login2FAAsync(tfaToken, tfaCode);
			await AuthenticateAsync(tfaLogin.access_token, tfaLogin.refresh_token);
		}
		public async Task InitializeUsingEmailCode(string identifier, string code)
		{
			var loginViaEmailCode = await LoginViaEmailCodeAsync(identifier, code);
			await AuthenticateAsync(loginViaEmailCode.access_token, loginViaEmailCode.refresh_token);
		}
		public async Task InitializeUsingTokensAsync(string accessToken, string refreshToken)
		{
			await AuthenticateAsync(accessToken, refreshToken);
		}
		#endregion

		#region API endpoints

		#region Without auth
		public async Task<GlobalResponse?> GetGlobalAsync()
			=> await SendRequest<GlobalResponse>(HttpMethod.Get, $"v1/globals", false);

		public async Task<LoginResponse?> LoginAsync(string email, string password)
			=> await SendRequest<LoginResponse>(HttpMethod.Post, $"v1/investor/login", false, new
			{
				email = email,
				password = password,
			});

		public async Task<TFAResponse?> Login2FAAsync(string tfaToken, string tfaCode)
			=> await SendRequest<TFAResponse>(HttpMethod.Post, $"v1/investor/login/2fa", false, new
			{
				code = tfaCode,
				tfa_token = tfaToken,
			});

		public async Task<EmailCodeResponse?> LoginViaEmailCodeAsync(string identifier, string code)
			=> await SendRequest<EmailCodeResponse>(HttpMethod.Post, $"v1/investor/login/email-code", false, new
			{
				identifier = identifier,
				code = code,
			});

		public async Task<RefreshTokenResponse?> RefreshTokenAsync(string accessToken, string refreshToken)
			=> await SendRequest<RefreshTokenResponse>(HttpMethod.Post, $"v1/investor/refresh-token", false,
				new
				{
					access_token = accessToken,
					refresh_token = refreshToken,
				});
		#endregion

		#region With auth
		public async Task<PostLoanResponse?> BuyLoan(int loanId, decimal amount)
			=> await SendRequest<PostLoanResponse>(HttpMethod.Post, $"v1/loans/{loanId}", true, new
			{
				amount = amount,
			});

		public async Task<BalanceMainReponse?> GetBalanceMainAsync()
			=> await SendRequest<BalanceMainReponse>(HttpMethod.Get, "v2/investor/balance/main", true);
		public async Task<ProfileResponse?> GetProfileAsync()
			=> await SendRequest<ProfileResponse>(HttpMethod.Get, $"v2/investor/profile", false);

		public async Task<LoansResponse?> GetLoansAsync(int offset = 0, int pageSize = 40, Dictionary<string, string?>? searchParams = null, string sort = "-loanId")
		{
			var url = $"v1/{_userPublicId}/loans?sort={sort}&offset={offset}&pageSize={pageSize}";
			if (searchParams != null)
				url = QueryHelpers.AddQueryString(url, searchParams);

			return await SendRequest<LoansResponse>(HttpMethod.Get, url, true);
		}

		public async Task<InvestmentsResponse?> GetInvestmentsAsync(int offset = 0, int pageSize = 40, Dictionary<string, string?>? searchParams = null, string sort = "-loanId")
		{
			var url = $"/v1/investor/investments?sort={sort}&offset={offset}&pageSize={pageSize}";
			if (searchParams != null)
				url = QueryHelpers.AddQueryString(url, searchParams);

			return await SendRequest<InvestmentsResponse>(HttpMethod.Get, url, true);
		}
		#endregion

		public async Task<T?> SendCustomApiAsync<T>(HttpMethod method, string url, bool isAuthorized, object? body = null) => await SendRequest<T>(method, url, isAuthorized, body);

		#endregion

		#region Private functions
		private void ValidateAuth()
		{
			if (!IsAuth())
				throw new Exception($"Authentication not inicialized.");
		}

		private async Task AuthenticateAsync(string accessToken, string refreshToken)
		{
			_accessToken = accessToken;
			_refreshToken = refreshToken;

			var profile = await GetProfileAsync();
			_userPublicId = profile.publicId;
		}

		static private bool _wasRetryWithToken = false;

		private async Task<T?> SendRequest<T>(HttpMethod method, string url, bool isAuthorized, object? body = null)
		{
			if (isAuthorized)
				ValidateAuth();
start:
			try
			{
				return await _peerBerryProxyApi.SendRequest<T>(method, url, _accessToken, body);
			}
			catch (UnauthorizedAccessException)
			{
				//Refresh mechanism
				if (!_wasRetryWithToken)
				{
					_wasRetryWithToken = true;
					var refresh = await RefreshTokenAsync(_accessToken, _refreshToken);
					await InitializeUsingTokensAsync(refresh.access_token, refresh.refresh_token);
					goto start;
				}
				throw;
			}
		}

		private bool IsAuth() => _userPublicId != null;
		#endregion

		private string? _email;
		private string? _password;
		private string? _accessToken;
		private string? _refreshToken;
		private string? _userPublicId;
		private readonly PeerBerryProxyApi _peerBerryProxyApi;


		public void Dispose()
		{
			_peerBerryProxyApi.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
