using Android.App;
using Android.Widget;
using Android.Graphics;

using Xamarin.Forms.Platform.Android;

using MCCForms;
using MCCForms.Droid;

[assembly: Xamarin.Forms.ExportRenderer (typeof(LabelRegularFont), typeof(LabelRegularFontCustomRenderer))]

namespace MCCForms.Droid
{
	public class LabelRegularFontCustomRenderer : LabelRenderer
	{
		Typeface _typeFaceRegular = Typeface.CreateFromAsset (Application.Context.Assets, "RijksoverheidSansTextTT-Regular_2_0.ttf");

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
			var nativeTextView = (TextView)Control;

			nativeTextView.SetTypeface (_typeFaceRegular, TypefaceStyle.Normal);
		}
	}
}