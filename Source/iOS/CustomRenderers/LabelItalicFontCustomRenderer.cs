using System;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using MCCForms;
using MCCForms.iOS;

[assembly: ExportRenderer (typeof (LabelItalicFont), typeof (LabelItalicFontCustomRenderer))]

namespace MCCForms.iOS
{
	public class LabelItalicFontCustomRenderer : LabelRenderer
	{
		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			var nativeLabel = (UILabel)Control;
			nativeLabel.Font = UIFont.FromName("RijksoverheidSansTextTT-Italic", nativeLabel.Font.PointSize);
		}
	}
}