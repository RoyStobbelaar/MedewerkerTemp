using Android.OS;
using Android.Graphics.Drawables;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using MCCForms.Droid;

[assembly: ExportRenderer(typeof(Switch), typeof(SwitchCustomRenderer))]
namespace MCCForms.Droid
{
	public class SwitchCustomRenderer : SwitchRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
		{
			base.OnElementChanged(e);

			int currentapiVersion = (int)Build.VERSION.SdkInt;

			if (currentapiVersion < Build.VERSION_CODES.L) {
				
				if (Control != null) {
					Control.TextOn = "Ja";
					Control.TextOff = "Nee";
					Control.SetTextColor (Android.Graphics.Color.Black);

					Android.Graphics.Color colorOn = AppConfigConstants.AppTheme.ColorSwitch.ToAndroid ();
					Android.Graphics.Color colorOff = Android.Graphics.Color.LightGray;
					Android.Graphics.Color colorDisabled = Android.Graphics.Color.Gray;

					StateListDrawable drawable = new StateListDrawable ();
					drawable.AddState (new int[] { Android.Resource.Attribute.StateChecked }, new ColorDrawable (colorOn));
					drawable.AddState (new int[] { -Android.Resource.Attribute.StateEnabled }, new ColorDrawable (colorDisabled));
					drawable.AddState (new int[] { }, new ColorDrawable (colorOff));

					Control.ThumbDrawable = drawable;
				}
			}
		}
	}
}