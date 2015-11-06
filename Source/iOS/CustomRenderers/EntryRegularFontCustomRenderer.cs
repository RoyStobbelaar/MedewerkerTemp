using System;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using MCCForms;
using MCCForms.iOS;

[assembly: ExportRenderer (typeof (EntryRegularFont), typeof (EntryRegularFontCustomRenderer))]

namespace MCCForms.iOS
{
	public class EntryRegularFontCustomRenderer : EntryRenderer
	{
		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			var nativeTextField = (UITextField)Control;
			nativeTextField.BorderStyle = UITextBorderStyle.None;
			nativeTextField.AutocapitalizationType = UITextAutocapitalizationType.Sentences;

			int fontSize = 14;

			if (Device.Idiom == TargetIdiom.Tablet) {
				fontSize = 17;
			}

			nativeTextField.Font = UIFont.FromName ("RijksoverheidSansTextTT-Regular", fontSize);//nativeTextField.Font.PointSize);
		}
	}
}