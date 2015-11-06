using System.Drawing;

using UIKit;
using CoreGraphics;

using Xamarin.Forms;

using MCCForms.iOS;

[assembly: Dependency (typeof(Dialog_iOS))] //How to use in Forms project: DependencyService.Get<IDialog> ().ShowProgressDialog ("The message you want to show");

namespace MCCForms.iOS
{
	public class Dialog_iOS : IDialog
	{
		static LoadingOverlay _loadingOverlay;

		public void ShowProgressDialog (string message)
		{
			if (_loadingOverlay != null) {
				HideProgressDialog ();
			}

			var numberOfWindows = UIApplication.SharedApplication.Windows.Length;
			int indexOfRootWindow = numberOfWindows - 1;
			UIViewController rootViewController = UIApplication.SharedApplication.Windows[indexOfRootWindow].RootViewController;
			UIViewController childViewController = rootViewController.PresentedViewController;

			CGRect bounds = childViewController != null ? childViewController.View.Bounds : rootViewController.View.Bounds;
			_loadingOverlay = new LoadingOverlay (new RectangleF ((float)bounds.X, (float)bounds.Y, (float)bounds.Width, (float)bounds.Height), message);

			if (childViewController != null) {
				childViewController.View.Add (_loadingOverlay);
			} else {
				rootViewController.View.Add (_loadingOverlay);
			}
		}

		public void HideProgressDialog()
		{
			_loadingOverlay.Hide ();
			_loadingOverlay = null;
		}
	}
}