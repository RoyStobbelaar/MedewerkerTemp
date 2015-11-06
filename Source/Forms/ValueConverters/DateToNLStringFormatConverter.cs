using System;
using System.Globalization;

/// <summary>
/// Date to NL string format converter.
/// Converts DateTime to NL localized string representation
/// </summary>

using Xamarin.Forms;

namespace MCCForms
{
	public class DateToNLStringFormatConverter : IValueConverter
	{
		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is DateTime)  {
				var dateTimeToShow = (DateTime)value;

				return dateTimeToShow.ToString("d", CultureInfo.CreateSpecificCulture("nl-NL"));
			}
			return value;
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}
	}
}