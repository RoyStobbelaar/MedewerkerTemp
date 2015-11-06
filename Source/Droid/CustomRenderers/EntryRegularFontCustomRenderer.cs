using Android.App;
using Android.Text;
using Android.Widget;
using Android.Graphics;

using Xamarin.Forms.Platform.Android;

using MCCForms;
using MCCForms.Droid;

[assembly: Xamarin.Forms.ExportRenderer (typeof(EntryRegularFont), typeof(EntryRegularFontCustomRenderer))]

namespace MCCForms.Droid
{
	public class EntryRegularFontCustomRenderer : EntryRenderer
	{
		Typeface _typeFaceRegular = Typeface.CreateFromAsset (Application.Context.Assets, "RijksoverheidSansTextTT-Regular_2_0.ttf");

		protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.Entry> e)
		{
			base.OnElementChanged (e);

			var nativeEditText = (EditText)Control;

			//Set custom font
			nativeEditText.SetTypeface (_typeFaceRegular, TypefaceStyle.Normal);

			//Set text size
			if (MCCForms.DeviceTypeHelper.IsTablet) {
				nativeEditText.TextSize = 17;
			} else {
				nativeEditText.TextSize = 14;
			}

			//Set styling
			nativeEditText.TextAlignment = Android.Views.TextAlignment.ViewStart; //Text alignment
			nativeEditText.SetPadding (0, 0, 0, 0);
			nativeEditText.InputType = InputTypes.TextFlagCapSentences; //Capitalize first letter of words
			nativeEditText.SetBackgroundResource(0); //Removes default blue underline from EditText
		}
	}
}