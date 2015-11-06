using System.Drawing;

using UIKit;

namespace MCCForms.iOS
{
	public class LoadingOverlay : UIView {

		UIActivityIndicatorView _activitySpinner;
		UILabel _loadingLabel;

		public LoadingOverlay (RectangleF frame, string message) : base (frame)
		{
			BackgroundColor = UIColor.Black;
			Alpha = 0.60f;
			AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

			float labelHeight = 22;
			float labelWidth = (float)Frame.Width - 20;

			float centerX = (float)Frame.Width / 2;
			float centerY = (float)Frame.Height / 2;

			_activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);

			_activitySpinner.Frame = new RectangleF (
				centerX - ((float)_activitySpinner.Frame.Width / 2) ,
				centerY - (float)_activitySpinner.Frame.Height - 20 ,
				(float)_activitySpinner.Frame.Width ,
				(float)_activitySpinner.Frame.Height);
			
			_activitySpinner.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;

			AddSubview (_activitySpinner);

			_activitySpinner.StartAnimating ();

			_loadingLabel = new UILabel(new RectangleF (
				centerX - (labelWidth / 2),
				centerY + 20 ,
				labelWidth ,
				labelHeight
			));

			_loadingLabel.BackgroundColor = UIColor.Clear;
			_loadingLabel.TextColor = UIColor.White;
			_loadingLabel.Text = message;
			_loadingLabel.TextAlignment = UITextAlignment.Center;
			_loadingLabel.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			_loadingLabel.Font = UIFont.FromName ("RijksoverheidSansTextTT-Regular", _loadingLabel.Font.PointSize);
			
			AddSubview (_loadingLabel);
		}

		public void Hide ()
		{
			UIView.Animate (
				0.5, // duration
				() => { Alpha = 0; },
				() => { RemoveFromSuperview(); }
			);
		}
	}
}
