using System;

using System.Threading.Tasks;

using Newtonsoft.Json;

using ModernHttpClient;
using System.Net.Http;

namespace MCCForms
{
	//https://developers.google.com/maps/articles/geocodestrat#client
	public class GoogleCoordHelper
	{
		const string BASE_URI = "http://maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}&sensor=false";

		public static async Task<AddressResult> GetAddressByCoordinates (double latitude, double longitude)
		{
			var client = new HttpClient(new NativeMessageHandler());

			try {
				string formattedLatitude = latitude.ToString ().Replace (",", ".");
				string formattedLongitude = longitude.ToString ().Replace (",", ".");

				string requestUri = string.Format (BASE_URI, formattedLatitude, formattedLongitude);

				var data = await client.GetStringAsync (requestUri);

				if (data != null) {

					var responseObject = JsonConvert.DeserializeObject<GoogleGeoCodeResponse> (data);

					string houseNumber = "";
					string streetName = "";
					string city = "";
					string province = "";
					string country = "";
					string countryAbre = "";
					string zipCode = "";

					foreach (var item in responseObject.results [0].address_components) {
						if (item.types [0].Equals ("street_number")) {
							houseNumber = item.long_name;
						} else if (item.types [0].Equals ("route")) {
							streetName = item.long_name;
						} else if (item.types [0].Equals ("locality")) {
							city = item.long_name;
						} else if (item.types [0].Equals ("administrative_area_level_1")) {
							province = item.long_name;
						} else if (item.types [0].Equals ("country")) {
							country = item.long_name;
							countryAbre = item.short_name;
						} else if (item.types [0].Equals ("postal_code_prefix") || item.types [0].Equals ("postal_code")) {
							zipCode = item.long_name;
						}
					}

					var returnAddressResult = new AddressResult {
						HouseNumber = houseNumber,
						Street = streetName,
						City = city,
						Province = province,
						Country = country,
						CountryAbbreviation = countryAbre,
						ZipCode = zipCode,
						Latitude = latitude,
						Longitude = longitude
					};
					client.Dispose ();
					return returnAddressResult;
				}

				client.Dispose ();
				return null;
			} catch (Exception ex) {
				client.Dispose ();
				return null;
			}
		}
	}

	//Classes needed to serialize json
	public class GoogleGeoCodeResponse
	{
		public string status { get; set; }
		public results[] results { get; set; }
	}

	public class results
	{
		public string formatted_address { get; set; }
		public geometry geometry { get; set; }
		public string[] types { get; set; }
		public address_component[] address_components { get; set; }
	}

	public class geometry
	{
		public string location_type { get; set; }
		public location location { get; set; }
	}

	public class location
	{
		public string lat { get; set; }
		public string lng { get; set; }
	}

	public class address_component
	{
		public string long_name { get; set; }
		public string short_name { get; set; }
		public string[] types { get; set; }
	}
}