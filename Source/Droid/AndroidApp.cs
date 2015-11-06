using System;

using Android.App;
using Android.Runtime;
using Xamarin;

namespace MCCForms.Droid
{
	[Application(Label = "MCC Forms Template", Theme = "@android:style/Theme.Holo.Light")]
    public class AndroidApp : Application
    {
		static AndroidApp _context;

		public AndroidApp (IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

		public AndroidApp ()
        {
        }

        public override void OnCreate ()
        {
            base.OnCreate();

			SetAppSettings ();
        }

		void SetAppSettings()
		{
			_context = this;

			//Initialize Xamarin Insights
			if (!string.IsNullOrEmpty (AppConfigConstants.XamarinInsightsApiKey)) {
				Insights.Initialize (AppConfigConstants.XamarinInsightsApiKey, _context);
			}

			if (AppConfigConstants.IsCertificatePinningEnabled) {
				//Forces to call ServicePointManager.ServerCertificateValidationCallback for every https web request
				Environment.SetEnvironmentVariable ("MONO_TLS_SESSION_CACHE_TIMEOUT", "0");
			}
		}

		public static AndroidApp GetAppContext()
		{
			return _context;
		}
    }
}