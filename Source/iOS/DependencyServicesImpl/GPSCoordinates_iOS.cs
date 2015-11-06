using System;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Geolocation;

using MCCForms.iOS;

[assembly: Xamarin.Forms.Dependency (typeof (GPSCoordinates_iOS))] //How to use in Forms project: DependencyService.Get<IGPSCoordinates> ().GetLocation();

namespace MCCForms.iOS
{
	public class GPSCoordinates_iOS : IGPSCoordinates
	{
		readonly Geolocator _locator;

		public GPSCoordinates_iOS()
		{
			_locator = new Geolocator ();
		}

		public async Task<GPSCoordinatesResponse> GetLocation ()
		{
			try{
				_locator.DesiredAccuracy = 50;

				Position position = await _locator.GetPositionAsync (timeout: 7500);

				return position != null ? new GPSCoordinatesResponse(position.Latitude, position.Longitude) : null;
			}catch {
				return null;
			}
		}

		public bool IsLocationServiceEnabled()
		{
			return _locator.IsGeolocationEnabled;
		}
	}
}