using System;

/// <summary>
/// Date to NL string format converter.
/// Converts double value to an integer
/// </summary>

using Xamarin.Forms;

namespace MCCForms
{
	public class DoubleToIntValueConverter : IValueConverter
	{
		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is double)  {
				var doubleValue = (double)value;

				var intValue = (int)doubleValue;

				return intValue;
			}
			return value;
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}
	}
}