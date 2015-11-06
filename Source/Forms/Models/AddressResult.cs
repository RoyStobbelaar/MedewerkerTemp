
/// <summary>
/// Address result.
/// Used to parse values to an object when getting a location from Google based on your longitude and latitude 
/// </summary>

namespace MCCForms
{
	public class AddressResult
	{
		public string HouseNumber { get; set; }
		public string Street { get; set; }
		public string City { get; set; }

		public string ZipCode { get; set; }
		public string Country { get; set; }
		public string CountryAbbreviation { get; set; }

		public string Province { get; set; }

		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}