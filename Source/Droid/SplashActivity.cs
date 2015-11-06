using System.Threading;

using Android.OS;
using Android.App;
using Android.Content.PM;

namespace MCCForms.Droid
{
	[Activity (Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.Orientation)]
	public class SplashActivity : Activity
	{
		bool _orientationAlreadyChanged;

		public SplashActivity ()
		{
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetOrientationBasedOnDeviceType ();

			SetContentView (Resource.Layout.splash);
			ThreadPool.QueueUserWorkItem (doSomething => LoadActivity ());
		}

		private void LoadActivity() {
			Thread.Sleep (600); // Simulate a long pause
			RunOnUiThread (() => StartActivity (typeof(MainActivity)));
		}

		private void SetOrientationBasedOnDeviceType ()
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