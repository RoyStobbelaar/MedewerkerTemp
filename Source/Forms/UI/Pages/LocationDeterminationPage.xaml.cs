using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MCCForms
{
	public partial class LocationDeterminationPage : ContentPage
	{
		public event EventHandler LocationChangedEvent;

		public static readonly BindableProperty AddressProperty = BindableProperty.Create<LocationDeterminationPage, string>(p => p.Address, default(string),defaultBindingMode: BindingMode.TwoWay);

		public string Address {
			get { 
				return (string)GetValue(AddressProperty); 
			}
			set {
				SetValue(AddressProperty, value); 
			}
		}

		public static readonly BindableProperty ZipcodeAndCityProperty = BindableProperty.Create<LocationDeterminationPage, string>(p => p.ZipcodeAndCity, default(string),defaultBindingMode: BindingMode.TwoWay);

		public string ZipcodeAndCity {
			get {
				return (string)GetValue(ZipcodeAndCityProperty); 
			}
			set {
				SetValue(ZipcodeAndCityProperty, value); 
			}
		}

		public LocationDeterminationPage (string title)
		{
			InitializeComponent ();
		
			BindingContext = this;	

			Title = title;

			labelProgress.Text = "Uw locatie wordt bepaald.\nEen ogenblik geduld a.u.b...";

			DetermineLocation ();

			if (DeviceTypeHelper.IsTablet) {
				labelProgress.FontSize = 15;
				labelLocationTitle.FontSize = 15;
				labelAddress.FontSize = 15;
				labelZipcodeAndCity.FontSize = 15;
			}
		}

		async void DetermineLocation()
		{
			var currentLocation = await DependencyService.Get<IGPSCoordinates> ().GetLocation();

			AddressResult geoLocatedAddress = null;

			if (currentLocation != null) {
				geoLocatedAddress = await GoogleCoordHelper.GetAddressByCoordinates (currentLocation.Latitude, currentLocation.Longitude);
			}

			if (currentLocation != null && geoLocatedAddress != null) {

				PositionPinOnMap (currentLocation.Latitude, currentLocation.Longitude);

				Address = geoLocatedAddress.Street + " " + geoLocatedAddress.HouseNumber;
				ZipcodeAndCity = geoLocatedAddress.ZipCode + ", " + geoLocatedAddress.City;

				//Notify the listeners that the location is changed
				EventHandler handler = LocationChangedEvent;
				if (handler != null) {
					handler (this, new LocationChangedEventArgs {
						Address = this.Address, ZipcodeAndCity = this.ZipcodeAndCity
					});
				}
			} 
			else {
				labelProgress.Text = "Uw locatie kon niet worden bepaald.";
				await DisplayAlert ("Foutmelding", "Uw huidige locatie kon niet bepaald worden. Heropen de pagina om een nieuwe poging te doen.", "OK");
			}
		}

		void PositionPinOnMap(double latitude, double longitude)
		{
			MapView.MoveToRegion (MapSpan.FromCenterAndRadius (new Position (latitude, longitude), Distance.FromMeters (500)));
			
			var pin = new Pin {
				Type = PinType.Place,
				Position = new Position(latitude, longitude),
				Label = "Uw locatie volgens GPS"
			};
			
			MapView.Pins.Add(pin);

			mapViewContainer.IsVisible = true;
		}
	}

	public class LocationChangedEventArgs : EventArgs
	{
		public string Address { get; set; }
		public string ZipcodeAndCity { get; set; }
	}
}