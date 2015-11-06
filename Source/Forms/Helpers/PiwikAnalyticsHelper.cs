using Xamarin.Forms;

namespace MCCForms
{
	public static class PiwikAnalyticsHelper //TODO for developers - add events you want to track
	{
		public static void TrackHomePage()
		{
			DependencyService.Get<IPiwikAnalytics> ().TrackView ("home.page");
		}

		public static void TrackMyButtonClickEvent()
		{
			DependencyService.Get<IPiwikAnalytics> ().TrackEvent("btn.klik", "home.btn.myButton");
		}
	}
}