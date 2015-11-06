
/// <summary>
/// Token response.
/// Used to parse values to an object when getting tokens from an OpenID Connect Identity Server
/// </summary>

using SQLite;
using Newtonsoft.Json;

namespace MCCForms
{
	public class TokenResponse
	{
		[JsonIgnore]
		[PrimaryKey]
		public int Id {
			get;
			set;
		}

		[JsonProperty("access_token")]
		public string AccessToken {
			get;
			set;
		}

		[JsonProperty("refresh_token")]
		public string RefreshToken {
			get;
			set;
		}

		[JsonProperty("token_type")]
		public string TokenType {
			get;
			set;
		}

		[JsonProperty("expires_in")]
		public int ExpiresIn {
			get;
			set;
		}
	}
}