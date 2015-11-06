using Android.App;
using Android.Text;
using Android.Graphics;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using MCCForms;
using MCCForms.Droid;

[assembly: Xamarin.Forms.ExportRenderer (typeof(Button), typeof(ButtonCustomRenderer))]

namespace MCCForms.Droid
{
	public class ButtonCustomRenderer : ButtonRenderer
	{
		Typeface _typeFaceRegular = Typeface.CreateFromAsset (Android.App.Application.Context.Assets, "RijksoverheidSansTextTT-Regular_2_0.ttf");

		protected override void OnElementChanged (ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged (e);

			Control.SetTypeface (_typeFaceRegular, TypefaceStyle.Normal);
		}
	}
}