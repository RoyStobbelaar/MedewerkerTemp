using Android.App;
using Android.Widget;
using Android.Graphics;

using Xamarin.Forms.Platform.Android;

using MCCForms;
using MCCForms.Droid;

[assembly: Xamarin.Forms.ExportRenderer (typeof(LabelItalicFont), typeof(LabelItalicFontCustomRenderer))]

namespace MCCForms.Droid
{
	public class LabelItalicFontCustomRenderer : LabelRenderer
	{
		Typeface _typeFaceThin = Typeface.CreateFromAsset (Application.Context.Assets, "RijksoverheidSansTextTT-Italic_2_0.ttf");

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
			var nativeTextView = (TextView)Control;

			nativeTextView.SetTypeface (_typeFaceThin, TypefaceStyle.Normal);
		}
	}
}