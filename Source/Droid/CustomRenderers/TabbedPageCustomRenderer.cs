using System.ComponentModel;

using Android.App;
using Android.Views;
using Android.Graphics.Drawables;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using MCCForms.Droid;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageCustomRenderer))]

namespace MCCForms.Droid
{
	public class TabbedPageCustomRenderer : TabbedRenderer
	{
		private Activity activity;
		private bool _alreadyStyledView = false;

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			activity = this.Context as Activity;
		}

		protected override void OnWindowVisibilityChanged(ViewStates visibility)
		{
			base.OnWindowVisibilityChanged(visibility);
			if (!_alreadyStyledView)
			{
				ActionBar actionBar = activity.ActionBar;

				actionBar.SetStackedBackgroundDrawable(new ColorDrawable(AppConfigConstants.AppTheme.ColorTabbarBackground.ToAndroid ()));

				_alreadyStyledView = true; 
			}
		}
	}
}