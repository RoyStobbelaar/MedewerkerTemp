using System;
using System.Collections.Generic;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using MCCForms.iOS;

[assembly: ExportRenderer (typeof (NavigationPage), typeof (NavigationCustomRenderer))] //Custom renderer to set left toolbar item and custom font for title

namespace MCCForms.iOS
{
	public class NavigationCustomRenderer : NavigationRenderer
	{
		public override void PushViewController (UIViewController viewController, bool animated)
		{
			base.PushViewController (viewController, animated);

			if (TopViewController.NavigationItem.RightBarButtonItems.Length == 2) { //2 navigation bar buttons found - so align one item on the left side and one item on the right side
				List<UIBarButtonItem> leftButtons = new List<UIBarButtonItem> ();
				leftButtons.Add (TopViewController.NavigationItem.RightBarButtonItems [1]);
				TopViewController.NavigationItem.LeftBarButtonItems = leftButtons.ToArray ();

				List<UIBarButtonItem> rightButtons = new List<UIBarButtonItem> ();
				rightButtons.Add (TopViewController.NavigationItem.RightBarButtonItems [0]);
				TopViewController.NavigationItem.RightBarButtonItems = rightButtons.ToArray ();
			} 
			else if (TopViewController.NavigationItem.RightBarButtonItems.Length == 1){ //1 navigation bar button found - so align this item on the left side
				List<UIBarButtonItem> leftButtons = new List<UIBarButtonItem> ();
				leftButtons.Add (TopViewController.NavigationItem.RightBarButtonItems [0]);
				TopViewController.NavigationItem.LeftBarButtonItems = leftButtons.ToArray ();

				TopViewController.NavigationItem.RightBarButtonItems = new List<UIBarButtonItem> ().ToArray ();
			}

			//Remove text from back button
			TopViewController.NavigationItem.BackBarButtonItem = new UIBarButtonItem (" ", UIBarButtonItemStyle.Plain, null);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//Set custom font and font color for navigation bar title
			this.NavigationBar.TitleTextAttributes = new UIStringAttributes() 
			{
				Font = UIFont.FromName("RijksoverheidSansTextTT-Regular", 20),
				ForegroundColor = AppConfigConstants.AppTheme.ColorToolbarText.ToUIColor ()
			};

			//Set navigation bar item color (button)
			this.NavigationBar.TintColor  = AppConfigConstants.AppTheme.ColorToolbarButton.ToUIColor ();
		}
	}
}