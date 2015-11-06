using System;

using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using UIKit;
using Foundation;

namespace MCCForms.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			SetAppSettings ();

			Forms.Init ();
			FormsMaps.Init();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}

		void SetAppSettings()
		{
			//These UI settings cannot be set in the Xamarin.Forms project

			//Set UISwitch color
			UISwitch.Appearance.OnTintColor = AppConfigConstants.AppTheme.ColorSwitch.ToUIColor ();

			//Set UISlider color
			UISlider.Appearance.TintColor = AppConfigConstants.AppTheme.ColorSlider.ToUIColor ();


			//Initialize Xamarin Insights
			if (!string.IsNullOrEmpty (AppConfigConstants.XamarinInsightsApiKey)) {
				Insights.Initialize (AppConfigConstants.XamarinInsightsApiKey);
			}

			if (AppConfigConstants.IsCertificatePinningEnabled) {
				//Forces to call ServicePointManager.ServerCertificateValidationCallback for every https web request
				Environment.SetEnvironmentVariable ("MONO_TLS_SESSION_CACHE_TIMEOUT", "0");
			}

			VersionHelper.AddVersionNumberToIosSettingsScreen();
		}
	}
}