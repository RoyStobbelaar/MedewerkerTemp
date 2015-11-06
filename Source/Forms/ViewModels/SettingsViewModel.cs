using System.Windows.Input;

using Xamarin.Forms;

namespace MCCForms
{
	public class SettingsViewModel : BaseViewModel
	{
		public ICommand OpenPincodePageCommand {
			get{
				return new Command (() => OpenPincodePage ());
			}
		}

		string _operatingSystem;
		public string OperatingSystem {
			get {
				return _operatingSystem;
			}
			set {
				if (_operatingSystem != value) {
					_operatingSystem = value;
					RaisePropertyChanged ();
				}
			}
		}

		string _appVersion;
		public string AppVersion {
			get {
				return _appVersion;
			}
			set {
				if (_appVersion != value) {
					_appVersion = value;
					RaisePropertyChanged ();
				}
			}
		}

		public SettingsViewModel ()
		{
			OperatingSystem = Device.OS.ToString () + " " + DependencyService.Get<IDeviceAndAppInformation> ().GetOperationSystemVersionNumber ();

			AppVersion = DependencyService.Get<IDeviceAndAppInformation> ().GetAppVersionNumber ();
		}

		void OpenPincodePage()
		{
			App.Current.MainPage = new PincodePage (PincodePageType.ChangePincodeStep1);
		}
	}
}