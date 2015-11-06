using System;

namespace MCCForms
{
	public interface IPiwikAnalytics
	{
		void TrackView(string viewName);

		void TrackEvent(string categoryName, string eventName);
	}
}