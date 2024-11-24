using System.Net;
using System.Net.Http.Json;

namespace PeerBerry.API
{
	public class PeerBerryProxyApi : IDisposable
	{
		private HttpClient _httpClient;

		public PeerBerryProxyApi()
		{
			var clientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli };
			_httpClient = new HttpClient(clientHandler)
			{
				BaseAddress = new Uri("https://api.peerberry.com/"),
			};
		}

		public async Task<T?> SendRequest<T>(HttpMethod method, string url, string accessToken, object? body = null)
		{
			HttpRequestMessage request = new HttpRequestMessage(method, url)
			{
				Headers = {
					Authorization = accessToken != null ? new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken) : null,
				},
				Content = body != null ? JsonContent.Create(body) : null,
			};
			request.Headers.Add("Accept", "application/json; charset=UTF-8");
			request.Headers.Add("Origin", "https://peerberry.com");
			request.Headers.Add("Referer", "https://peerberry.com/en/client/overview");
			request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36");

			var response = await _httpClient.SendAsync(request);
			if (response.StatusCode != HttpStatusCode.OK)
			{
				if (response.StatusCode == HttpStatusCode.Unauthorized)
				{
					throw new UnauthorizedAccessException(await response.Content.ReadAsStringAsync());
				}
				//TODO: log
				throw new Exception(await response.Content.ReadAsStringAsync());
			}
			return await response.Content.ReadFromJsonAsync<T>();
		}

		public void Dispose()
		{
			((IDisposable)_httpClient).Dispose();
		}
	}
}
