using Foundation;

using Xamarin.Forms;

using Org.Piwik.Sdk;

using MCCForms;
using MCCForms.iOS;

[assembly: Dependency (typeof(PiwikAnalytics_iOS))] //How to use in Forms project: DependencyService.Get<IPiwikAnalytics> ().TrackHomepageOpened();

namespace MCCForms.iOS
{
	public class PiwikAnalytics_iOS : IPiwikAnalytics
	{
		static PiwikTracker _piwikTracker;

		public PiwikAnalytics_iOS ()
		{
			if (_piwikTracker == null) {
				_piwikTracker = PiwikTracker.SharedInstanceWithSiteID (AppConfigConstants.PiwikSiteId.ToString (), new NSUrl (AppConfigConstants.PiwikBaseUrl));
			}
		}

		public void TrackView(string viewName)
		{
			_piwikTracker.SendView (viewName);
		}

		public void TrackEvent(string categoryName, string eventName)
		{
			_piwikTracker.SendEventWithCategory (categoryName, eventName, null, null);
		}
	}
}