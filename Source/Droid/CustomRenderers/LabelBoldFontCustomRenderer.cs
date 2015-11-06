using Android.App;
using Android.Widget;
using Android.Graphics;

using Xamarin.Forms.Platform.Android;

using MCCForms;
using MCCForms.Droid;

[assembly: Xamarin.Forms.ExportRenderer (typeof(LabelBoldFont), typeof(LabelBoldFontCustomRenderer))]

namespace MCCForms.Droid
{
	public class LabelBoldFontCustomRenderer : LabelRenderer
	{
		Typeface _typeFaceBold = Typeface.CreateFromAsset (Application.Context.Assets, "RijksoverheidSansTextTT-Bold_2_0.ttf");

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
			var nativeTextView = (TextView)Control;

			nativeTextView.SetTypeface (_typeFaceBold, TypefaceStyle.Normal);
		}
	}
}