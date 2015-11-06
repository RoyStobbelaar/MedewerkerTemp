using System;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using MCCForms;
using MCCForms.iOS;

[assembly: ExportRenderer (typeof (EditorRegularFont), typeof (EditorRegularFontCustomRenderer))]

namespace MCCForms.iOS
{
	public class EditorRegularFontCustomRenderer : EditorRenderer
	{
		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			var nativeTextView = (UITextView)Control;
			nativeTextView.Font = UIFont.FromName("RijksoverheidSansTextTT-Regular", nativeTextView.Font.PointSize);
		}
	}
}