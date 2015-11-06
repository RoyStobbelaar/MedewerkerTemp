using System;

using Xamarin.Forms;

namespace MCCForms
{
	public partial class LoginPage : ContentPage
	{
		Rectangle originalButtonLayoutBounds;

		public LoginPage ()
		{
			InitializeComponent ();

			ChangeAppearenceBasedOnDeviceType ();

			BindingContext = new LoginViewModel ();

			SetColorTheme (AppConfigConstants.AppTheme);

			navigationButtonLayout.IsVisible = false;
			//navigationButtonLayout.Image.Source = "sluiten.png";
			//navigationButtonLayout.Image.Scale = 0.4;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			originalButtonLayoutBounds = layoutButtonHolder.Bounds;

			entryUsername.Focused += OnKeyboardAppeared;
			entryUsername.Unfocused += OnKeyboardDisappeared;

			entryPassword.Focused += OnKeyboardAppeared;
			entryPassword.Unfocused += OnKeyboardDisappeared;
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();

			entryUsername.Focused -= OnKeyboardAppeared;
			entryUsername.Unfocused -= OnKeyboardDisappeared;

			entryPassword.Focused -= OnKeyboardAppeared;
			entryPassword.Unfocused -= OnKeyboardDisappeared;
		}

		void ChangeAppearenceBasedOnDeviceType ()
		{
			if (DeviceTypeHelper.IsPhone) {
				labelHeaderTitle.FontSize = 24;
				labelInfo.FontSize = 14;
			}
			else if (DeviceTypeHelper.IsTablet) {
				labelHeaderTitle.FontSize = 32;
				labelInfo.FontSize = 15;

				labelUsername.FontSize = 15;
				labelPassword.FontSize = 15;

				buttonLogin.FontSize = 15;

				gridBackground.RowDefinitions [0] = new RowDefinition { Height = 90 };
			}
		}

		void SetColorTheme (ColorTheme appTheme)
		{
			labelHeaderTitle.TextColor = appTheme.ColorPincodePageTitleH1;
		}

		void OnKeyboardAppeared(object sender, FocusEventArgs e)
		{
			var yPosition = gridPassword.Y + gridPassword.Height + 10;

			var newBounds = new Rectangle (originalButtonLayoutBounds.X, yPosition, layoutButtonHolder.Width, layoutButtonHolder.Height);
		
			layoutButtonHolder.LayoutTo (newBounds, 180, Easing.Linear);

			scrollView.ScrollToAsync (0, gridUserName.Y, true);
		}

		void OnKeyboardDisappeared(object sender, FocusEventArgs e)
		{
			double yPosition = this.masterLayout.Height - (layoutButtonHolder.Height + gridBackground.Height);

			var newBounds = new Rectangle (originalButtonLayoutBounds.X, yPosition, layoutButtonHolder.Width, layoutButtonHolder.Height);

			layoutButtonHolder.LayoutTo (newBounds, 180, Easing.Linear);

			scrollView.ScrollToAsync (0, 0, true);
		}
	}
}