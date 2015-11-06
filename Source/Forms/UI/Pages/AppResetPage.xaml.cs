using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MCCForms
{
	public partial class AppResetPage : ContentPage
	{
		public AppResetPage ()
		{
			InitializeComponent ();

			BindingContext = new AppResetViewModel ();

			ChangeAppearenceBasedOnDeviceType ();

			SetColorTheme (AppConfigConstants.AppTheme);

			navigationButtonLayout.Image.Source = "";
		}

		void ChangeAppearenceBasedOnDeviceType ()
		{
			if (DeviceTypeHelper.IsPhone) {
				labelHeaderTitle.FontSize = 24;
				labelInfo.FontSize = 14;
				buttonConfigureApp.FontSize = 14;
			} else if (DeviceTypeHelper.IsTablet) {
				labelHeaderTitle.FontSize = 32;
				labelInfo.FontSize = 15;
				buttonConfigureApp.FontSize = 15;

				gridBackground.RowDefinitions [0] = new RowDefinition { Height = 90 };
			}
		}

		void SetColorTheme (ColorTheme appTheme)
		{
			labelHeaderTitle.TextColor = appTheme.ColorPincodePageTitleH1;
		}
	}
}