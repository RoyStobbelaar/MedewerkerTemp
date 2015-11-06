using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using UIKit;

using MCCForms.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageCustomRenderer))]

namespace MCCForms.iOS
{
	public class TabbedPageCustomRenderer : TabbedRenderer
	{
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			//Set UITabBar colors
			TabBar.TintColor = AppConfigConstants.AppTheme.ColorTabbarSelectedItem.ToUIColor ();
			TabBar.BarTintColor = AppConfigConstants.AppTheme.ColorTabbarBackground.ToUIColor ();
			TabBar.BackgroundColor = AppConfigConstants.AppTheme.ColorTabbarUnselectedItem.ToUIColor ();
		}
	}
}