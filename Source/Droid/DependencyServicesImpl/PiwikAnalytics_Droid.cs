using Xamarin.Forms;

using Org.Piwik.Sdk;

using MCCForms.Droid;

[assembly: Dependency (typeof (PiwikAnalytics_Droid))] //How to use in Forms project: DependencyService.Get<IPiwikAnalytics> ().TrackHomepageOpened();

namespace MCCForms.Droid
{
	public class PiwikAnalytics_Droid : IPiwikAnalytics
	{
		static Tracker _piwikTracker;

		public PiwikAnalytics_Droid ()
		{
			if (_piwikTracker == null) {
				Piwik piwik = Piwik.GetInstance (AndroidApp.GetAppContext ());
				_piwikTracker = piwik.NewTracker(AppConfigConstants.PiwikBaseUrl, AppConfigConstants.PiwikSiteId);
			}
		}

		public void TrackView(string viewName)
		{
			_piwikTracker.TrackScreenView (viewName);
		}

		public void TrackEvent(string categoryName, string eventName)
		{
			_piwikTracker.TrackEvent (categoryName, eventName, null, null);
		}
	}
}