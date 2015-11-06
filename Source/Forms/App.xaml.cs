using System;
using System.Net;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MCCForms
{
	public partial class App : Application
	{
		SslValidator _sslValidator;
		bool _lockOrStartApp;

		static App ()
		{
			DependencyService.Register<MessageVisualizerService, MessageVisualizerService> ();
			DependencyService.Register<NavigationService, NavigationService> ();
		}

		public App ()
		{
			InitializeComponent ();

			SetGlobalDynamicResources ();

			_lockOrStartApp = true;

			CheckWhichPageShouldOpened ();

			SetCertificateValidator ();
		}

		void SetGlobalDynamicResources()
		{
			this.Resources ["ColorSecondToolbarBackground"] = AppConfigConstants.AppTheme.ColorSecondToolbarBackground;
			this.Resources ["ColorToolbarBackground"] = AppConfigConstants.AppTheme.ColorToolbarBackground;
			this.Resources ["ColorPageBackground"] = AppConfigConstants.AppTheme.ColorPageBackground;
			this.Resources ["ColorSearchbar"] = AppConfigConstants.AppTheme.ColorSearchbar;
			this.Resources ["ColorButton"] = AppConfigConstants.AppTheme.ColorButton;
		}

		protected async override void OnSleep ()
		{
			await ShowBackgroundEnteredPage ();

			AppLockHelper.SetOnSleepTime();
		}

		protected async override void OnResume ()
		{
			await HideBackgroundEnteredPage();

			if (AppLockHelper.ShouldLockApp) {
				DatabaseHelper.Instance.CloseAndForgetConnection ();

				_lockOrStartApp = true;

				CheckWhichPageShouldOpened ();
			}
		}

		async Task ShowBackgroundEnteredPage()
		{
			if (AppConfigConstants.HideContentWhenAppIsInBackground) {
				await DependencyService.Get<NavigationService> ().GoToModalPage (new BackgroundEnteredPage (), false);
			}
		}

		async Task HideBackgroundEnteredPage()
		{
			if (AppConfigConstants.HideContentWhenAppIsInBackground) {
				await DependencyService.Get<NavigationService> ().PopBackgroundEnteredPage ();
			}
		}
			
		void CheckWhichPageShouldOpened()
		{
			if (_lockOrStartApp) {
				if (AppConfigConstants.IsPincodeRequired) {

					if (DatabaseHelper.Instance.IsDatabaseAlreadyCreated) {
						OpenEnterExistingPincodePage ();
					} else {
						OpenSetNewPincodePage ();
					}
				} else {
					if (DatabaseHelper.Instance.OpenConnection ()) {
						OpenTabBarPage ();
					}
				}
			} else {
				OpenTabBarPage ();
			}
			_lockOrStartApp = false;
		}

		void SetCertificateValidator()
		{
			if (AppConfigConstants.IsCertificatePinningEnabled) {
				_sslValidator = new SslValidator ();
				ServicePointManager.ServerCertificateValidationCallback = _sslValidator.ValidateServerCertficate;

				_sslValidator.CertificateMismatchFound += async (object sender, EventArgs e) => {
					await DependencyService.Get<MessageVisualizerService> ().ShowMessage ("Foutmelding", "Er is een ongeldig certificaat geconstateerd. Hierdoor kan er geen verbinding worden gemaakt met de server", "OK");
				};
			}
		}

		void OpenSetNewPincodePage()
		{
			MainPage = new PincodePage (PincodePageType.NewPincode);
		}

		void OpenEnterExistingPincodePage()
		{
			MainPage = new PincodePage (PincodePageType.ExistingPincode);
		}

		void OpenTabBarPage()
		{
			MainPage = new TabBarPage ();
		}
	}
}