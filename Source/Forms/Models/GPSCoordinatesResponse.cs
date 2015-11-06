using System;

/// <summary>
/// GPS coordinates response.
/// Used to parse values to an object when determine your current locations
/// </summary>

namespace MCCForms
{
	public class GPSCoordinatesResponse
	{
		public DateTime UpdatedTime { set; get; }
		public double Latitude { set; get; }
		public double Longitude { set; get; }

		public GPSCoordinatesResponse(double latitude, double longitude)
		{
			UpdatedTime = DateTime.Now;
			Latitude = latitude;
			Longitude = longitude;
		}
	}
}