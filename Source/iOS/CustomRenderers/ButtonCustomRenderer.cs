using System;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using MCCForms;
using MCCForms.iOS;

[assembly: ExportRenderer (typeof (Button), typeof (ButtonCustomRenderer))]

namespace MCCForms.iOS
{
	public class ButtonCustomRenderer : ButtonRenderer
	{
		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			var nativeButton = (UIButton)Control;
			nativeButton.Font = UIFont.FromName("RijksoverheidSansTextTT-Regular", nativeButton.Font.PointSize);
		}
	}
}