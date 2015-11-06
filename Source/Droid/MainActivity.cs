using Android.OS;
using Android.App;
using Android.Content.PM;

using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace MCCForms.Droid
{
	[Activity (Icon = "@drawable/icon", Theme = "@style/MyLightTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

	public class MainActivity : FormsApplicationActivity
	{
		bool _orientationAlreadyChanged;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			if (AppConfigConstants.HideContentWhenAppIsInBackground) {
				Window.AddFlags (Android.Views.WindowManagerFlags.Secure);
			}

			SetOrientationBasedOnDeviceType ();

			Forms.Init (this, bundle);
			FormsMaps.Init (this, bundle);

			LoadApplication (new App ());
		}

		void SetOrientationBasedOnDeviceType ()
		{
			if (!_orientationAlreadyChanged) {
				if (DeviceTypeHelper.GetDeviceType () == DeviceType.Phone) {
					RequestedOrientation = ScreenOrientation.Portrait;
				}
			}
			_orientationAlreadyChanged = true;
		}
	}
}