using System;

namespace MCCForms
{
	public static class AppLockHelper
	{
		static DateTime? _onSleepDateTime;

		public static void SetOnSleepTime()
		{
			_onSleepDateTime = DateTime.Now;
		}

		public static bool ShouldLockApp {
			get {
				if (_onSleepDateTime == null) {
					return false;
				}
				else {
					double minutesInactive = (DateTime.Now - (DateTime)_onSleepDateTime).TotalMinutes;
					_onSleepDateTime = null;

					if (minutesInactive >= AppConfigConstants.MinutesBeforeLockingApp) {
						return true;
					} else {
						return false;
					}
				}
			}
		}
	}
}