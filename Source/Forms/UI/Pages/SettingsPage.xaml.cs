using Xamarin.Forms;

namespace MCCForms
{
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();

			BindingContext = new SettingsViewModel();

			if (!AppConfigConstants.IsPincodeRequired) {
				layoutChangePincode.IsVisible = false;
			}

			if (DeviceTypeHelper.IsTablet) {
				labelOSTitle.FontSize = 15;
				labelOS.FontSize = 15;

				labelAppVersionTitle.FontSize = 15;
				labelAppVersion.FontSize = 15;

				buttonChangePincode.FontSize = 15;
			}
		}
	}
}