
/// <summary>
/// User info response.
/// Used to parse values to an object when getting user info from an OpenID Connect Identity Server
/// </summary>

using Newtonsoft.Json;

namespace MCCForms
{
	public class UserInfoResponse
	{
		[JsonProperty("sub")]
		public string FullUsername
		{
			get;
			set;
		}

		[JsonProperty("name")]
		public string FullName
		{
			get;
			set;
		}

		[JsonProperty("email")]
		public string Email
		{
			get;
			set;
		}

		[JsonProperty("family_name")]
		public string LastName
		{
			get;
			set;
		}

		[JsonProperty("given_name")]
		public string FirstName
		{
			get;
			set;
		}

		[JsonProperty("role")]
		public string Role
		{
			get;
			set;
		}

		[JsonProperty("phone_number")]
		public string PhoneNumber
		{
			get;
			set;
		}

		[JsonProperty("department")]
		public string Department
		{
			get;
			set;
		}

		[JsonProperty("title")]
		public string JobTitle
		{
			get;
			set;
		}
	}
}