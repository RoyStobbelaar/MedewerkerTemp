using System;
using System.IO;
using System.Diagnostics;
using System.Globalization;

/// <summary>
/// Path to image source converter.
/// Converts image file path to an ImageSource
/// </summary>

using Xamarin.Forms;

namespace MCCForms
{
	public class PathToImageSourceConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try{
				var filePathString = (string) value;

				return FilesystemHelper.ImageSourceFromPath (filePathString);
			}
			catch(Exception ex) {
				Debug.WriteLine (ex.Message);

				return null;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}