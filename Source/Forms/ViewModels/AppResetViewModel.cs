using System.Windows.Input;

using Xamarin.Forms;

namespace MCCForms
{
	public class AppResetViewModel
	{
		public ICommand OpenPincodePageCommand { 
			get {
				return new Command (() => OpenPincodePage ());
			}
		}

		public int MaxNumberOfLoginAttempts {
			get {
				return AppConfigConstants.NumberOfLoginAttemptsBeforeAppWillBeReset;
			}
		}

		public AppResetViewModel ()
		{
		}

		void OpenPincodePage()
		{
			App.Current.MainPage = new PincodePage (PincodePageType.NewPincode);
		}
	}
}