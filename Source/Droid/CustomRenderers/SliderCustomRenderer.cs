using Android.Graphics.Drawables;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using MCCForms.Droid;
using Android.Graphics;

[assembly: ExportRenderer(typeof(Slider), typeof(SliderCustomRenderer))]
namespace MCCForms.Droid
{
	public class SliderCustomRenderer : SliderRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Slider> e)
		{
			base.OnElementChanged(e);

			if (Control != null) {

				Control.ProgressDrawable.SetColorFilter(AppConfigConstants.AppTheme.ColorSlider.ToAndroid (), PorterDuff.Mode.SrcIn);

				//Tip: you can use Control.SetThumb(new DrawableContainer()) 
			}
		}
	}
}