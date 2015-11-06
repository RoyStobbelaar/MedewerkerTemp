using System;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Geolocation;

using MCCForms.Droid;

[assembly: Xamarin.Forms.Dependency (typeof (GPSCoordinates_Droid))]

namespace MCCForms.Droid
{
	public class GPSCoordinates_Droid : IGPSCoordinates
	{
		readonly Geolocator _locator;

		public GPSCoordinates_Droid ()
		{
			_locator = new Geolocator (AndroidApp.GetAppContext ());
		}
			
		public async Task<GPSCoordinatesResponse> GetLocation ()
		{
			try{
				_locator.DesiredAccuracy = 50;

				Position position = await _locator.GetPositionAsync (timeout: 7500);

				return position != null ? new GPSCoordinatesResponse(position.Latitude, position.Longitude) : null;
			} catch {
				return null;
			}
		}

		public bool IsLocationServiceEnabled()
		{
			return _locator.IsGeolocationEnabled;
		}
	}
}