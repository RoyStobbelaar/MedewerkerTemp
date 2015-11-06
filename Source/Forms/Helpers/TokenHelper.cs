using System;
using System.Text;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace MCCForms
{
	public class TokenHelper
	{
		static TokenHelper _instance;

		public static TokenHelper Instance {
			get {
				if (_instance == null) {
					_instance = new TokenHelper ();

				}
				return _instance;
			}
		}

		public async Task<TokenResponse> RequestTokensAsync(string identityServerUrl, string clientId, string clientSecret, string username, string password)
		{
			//Just to test with servers with a self signed certificate
			//ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

			var formContent = new Dictionary<string, string> {
				{"grant_type", "password"},
				{"username", username},
				{"password", password},
				{"scope", AppConfigConstants.IdentityServerClientScope }
			};

			var tokenResponse = await RequestToken(identityServerUrl, clientId, clientSecret, formContent);
			if (tokenResponse != null) {

				StoreTokens (tokenResponse);
			}

			return tokenResponse;
		}

		public async Task<TokenResponse> RequestNewTokens(string identityServerUrl, string clientId, string clientSecret, string refreshToken)
		{
			var formContent = new Dictionary<string, string> {
				{"grant_type", "refresh_token"},
				{"refresh_token", refreshToken}
			};

			var tokenResponse = await RequestToken(identityServerUrl, clientId, clientSecret, formContent);

			if (tokenResponse != null) {

				StoreTokens (tokenResponse);
			}

			return tokenResponse;
		}

		public async Task<UserInfoResponse> GetUserInfo(string accessToken)
		{
			var httpClient = new HttpClient();
			httpClient.Timeout = TimeSpan.FromMilliseconds (5000);
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var response = await httpClient.GetAsync(AppConfigConstants.IdentityServerUserInfoUrl);

			if (response.IsSuccessStatusCode)
			{
				var result = response.Content.ReadAsStringAsync().Result;

				return JsonConvert.DeserializeObject<UserInfoResponse>(result);
			}
			return null;
		}

		async Task<TokenResponse> RequestToken(string identityServerUrl, string clientId, string clientSecret, Dictionary<string, string> formContent)
		{
			try{
				var formUrlEncodedContent = new FormUrlEncodedContent(formContent);
				var result = await PostWithBasicAuthentication(identityServerUrl, formUrlEncodedContent, clientId, clientSecret);
				var token = DeserializeTokenResponse(result);

				return token;
			} 
			catch (Exception ex){
				Debug.WriteLine (ex.Message);
				return null;
			}
		}
			
		async Task<string> PostWithBasicAuthentication(string url, FormUrlEncodedContent content, string username, string password)
		{
			try
			{
				var httpClient = SetupHttpClient(username, password);

				var response = await httpClient.PostAsync(url, content);
				if (response.IsSuccessStatusCode)
				{
					return await response.Content.ReadAsStringAsync();
				}
				return null;
			}
			catch (Exception ex){
				Debug.WriteLine (ex.Message);
				return null;
			}
		}

		static HttpClient SetupHttpClient(string clientIdAsUsername, string clientSecretAsPassword)
		{
			var httpClient = new HttpClient();
			httpClient.Timeout = TimeSpan.FromMilliseconds (5000);
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", clientIdAsUsername, clientSecretAsPassword))));

			return httpClient;
		}

		static TokenResponse DeserializeTokenResponse(string result)
		{
			if (string.IsNullOrEmpty(result))
				return null;

			return JsonConvert.DeserializeObject<TokenResponse>(result);
		}

		static void StoreTokens(TokenResponse tokenResponse)
		{
			//Store expiration date for access token
			var expirationDateAccessToken = DateTime.Now.AddSeconds (tokenResponse.ExpiresIn - 30);
			var expirationDateAccessTokenAsString = expirationDateAccessToken.ToString ();
			DatabaseHelper.Instance.UpdateSetting (new Setting {
				Key = StorageConstants.AccessTokenExpirationDateKey,
				Value = expirationDateAccessTokenAsString
			});

			//Store expiration date for refresh token
			var expirationDateRefreshToken = DateTime.Now.AddDays (364.5); //expires after a year (is set as configuration in the identity server)
			var expirationDateRefreshTokenAsString = expirationDateRefreshToken.ToString ();
			DatabaseHelper.Instance.UpdateSetting (new Setting {
				Key = StorageConstants.RefreshTokenExpirationDateKey,
				Value = expirationDateRefreshTokenAsString
			});

			//Store access token
			DatabaseHelper.Instance.UpdateSetting (new Setting {
				Key = StorageConstants.AccessTokenKey,
				Value = tokenResponse.AccessToken
			});

			//Store refresh token
			DatabaseHelper.Instance.UpdateSetting (new Setting {
				Key = StorageConstants.RefreshTokenKey,
				Value = tokenResponse.RefreshToken
			});
		}

		public bool IsAccessTokenExpired
		{
			get {
				var setting = DatabaseHelper.Instance.GetSetting (StorageConstants.AccessTokenExpirationDateKey);
				if (setting != null) {

					var expirationDateAsString = setting.Value;
					var expirationDate = Convert.ToDateTime (expirationDateAsString);

					if (expirationDate > DateTime.Now) {
						return true;
					}
				}
				return false;
			}
		}

		public bool IsRefreshTokenExpired //Means login page should be shown to the user
		{
			get {
				var setting = DatabaseHelper.Instance.GetSetting (StorageConstants.RefreshTokenExpirationDateKey);
				if (setting != null) {

					var expirationDateAsString = setting.Value;
					var expirationDate = Convert.ToDateTime (expirationDateAsString);

					if (expirationDate > DateTime.Now) {
						return true;
					}
				}
				return false;
			}
		}

		public string StoredAccessToken
		{
			get{
				var setting = DatabaseHelper.Instance.GetSetting (StorageConstants.AccessTokenKey);

				if (setting != null) {
					return setting.Value;
				}
				return null;
			}
		}

		public string StoredRefreshToken
		{
			get{
				var setting = DatabaseHelper.Instance.GetSetting (StorageConstants.RefreshTokenKey);
				if (setting != null) {
					return setting.Value;
				}
				return null;
			}
		}
	}
}