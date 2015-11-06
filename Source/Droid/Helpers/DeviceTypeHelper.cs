using System;
using System.Diagnostics;

using Android.App;
using Android.Util;

namespace MCCForms.Droid
{
	public enum DeviceType { Phone, Tablet}

	public class DeviceTypeHelper
	{
		public static DeviceType GetDeviceType()
		{
			DisplayMetrics displayMetrics = Application.Context.Resources.DisplayMetrics;

			double density = displayMetrics.Density * 160;
			double x = Math.Pow(displayMetrics.WidthPixels / density, 2);
			double y = Math.Pow(displayMetrics.HeightPixels / density, 2);
			double screenInches = Math.Sqrt(x + y);

			Debug.WriteLine (screenInches + " inch device detected");

			// Tablet devices should have a screen size greater than 6,5 inches
			if (screenInches < 6.5) {
				return DeviceType.Phone;
			} else {
				return DeviceType.Tablet;
			}
		}
	}
}