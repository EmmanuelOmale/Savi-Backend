using Savi.Api.Models;

namespace Savi.Api.Service
{
	public class GoogleSignupService : IGoogleSignupService
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpClientFactory _httpClientFactory;

		public GoogleSignupService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
		{
			_configuration = configuration;
			_httpClientFactory = httpClientFactory;
		}

		public async Task<UserProfile?> SignupWithGoogleAsync(string code)
		{
			var options = new GoogleOptions();
			_configuration.GetSection("Authentication:Google").Bind(options);

			var tokenUrl = $"https://oauth2.googleapis.com/token";
			var httpClient = _httpClientFactory.CreateClient();

			var tokenResponse = await httpClient.PostAsync(tokenUrl, new FormUrlEncodedContent(new Dictionary<string, string>
		{
			{ "code", code },
			{ "client_id", options.ClientId },
			{ "client_secret", options.ClientSecret },
			{ "redirect_uri", options.RedirectUri },
			{ "grant_type", "authorization_code" }
		}));

			if (tokenResponse.IsSuccessStatusCode)
			{
				var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
			}

			return null;
		}
	}
}