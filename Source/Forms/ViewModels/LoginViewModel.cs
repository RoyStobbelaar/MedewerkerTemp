using System.Windows.Input;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MCCForms
{
	public class LoginViewModel : BaseViewModel
	{
		public ICommand LoginCommand{
			get {
				return new Command (() => {
					Login();
				});
			}
		}

		public string InformationText{
			get {
				return "Deze app heeft verbinding nodig met het netwerk om te kunnen functioneren.\n\nAanmelding";
			}
		}

		string _username;
		public string Username {
			get {
				return _username;
			}
			set {
				if (_username != value) {
					_username = value;
					RaisePropertyChanged ();
				}
			}
		}

		string _password;
		public string Password {
			get {
				return _password;
			}
			set {
				if (_password != value) {
					_password = value;
					RaisePropertyChanged ();
				}
			}
		}

		public LoginViewModel ()
		{
		}

		async void Login()
		{
			if (string.IsNullOrEmpty (Username) || string.IsNullOrEmpty (Password)) {
				await ShowMessage ("Foutmelding", "Niet alle vereiste velden zijn ingevuld.");
			} 
			else {
				if (DependencyService.Get<IReachability> ().IsConnected ()) {

					var token = await TokenHelper.Instance.RequestTokensAsync (AppConfigConstants.IdentityServerTokenUrl, AppConfigConstants.IdentityServerClientId, AppConfigConstants.IdentityServerClientSecret, Username, Password);
					if (token == null) {
						await ShowMessage ("Foutmelding", "U heeft foutieve inloggegevens ingevoerd");

					} else {
						await ShowMessage ("Success", "U bent succesvol ingelogd");
					}
				} 
				else {
					await ShowMessage ("Foutmelding", "U heeft momenteel geen internet verbinding. Dit is noodzakelijk om te kunnen inloggen.");
				}
			}
		}

		async Task<bool>ShowMessage(string title, string message)
		{
			return await DependencyService.Get<MessageVisualizerService>().ShowMessage (title, message, "OK");
		}
	}
}