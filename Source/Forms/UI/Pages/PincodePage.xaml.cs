using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MCCForms
{
	public enum PincodePageType
	{
		NewPincode,
		ExistingPincode,
		ChangePincodeStep1,
		ChangePincodeStep2
	}

	public partial class PincodePage : ContentPage
	{
		PincodePageType _pincodePageType;
		string[] _pinNumbersFirstRow;
		string[] _pinNumbersSecondRow;
		int _activePinTile;
		int _numberOfPinTiles;
		Button _buttonInfo;
		bool _fingerPrintDialogAlreadyShown = false;

		public PincodePage (PincodePageType pincodePageType)
		{
			_pincodePageType = pincodePageType;

			InitializeComponent ();

			SetPageType ();

			ChangeAppearenceBasedOnDeviceType ();

			SetColorTheme (AppConfigConstants.AppTheme);

			_buttonInfo = navigationInfoButtonLayout.Button;
			_buttonInfo.Clicked += OnButtonInfoClicked;
		}

		void SetPageType ()
		{
			labelReEnterPin.Text = StringConstants.ReEnterNewPincode;
			_activePinTile = 0;

			if (_pincodePageType == PincodePageType.NewPincode || _pincodePageType == PincodePageType.ChangePincodeStep2) {
				MakePinRowActive (gridPinTiles1.PinTiles);
				MakePinRowInActive (gridPinTiles2.PinTiles);

				_pinNumbersFirstRow = new string[5];
				_pinNumbersSecondRow = new string[5];

				_numberOfPinTiles = 10;

				if (_pincodePageType == PincodePageType.NewPincode) {
					labelHeaderTitle.Text = "Gegevens beveiligen";
					labelEnterPin.Text = StringConstants.EnterPincode;
				} else {
					labelHeaderTitle.Text = "Pincode wijzigen";
					labelEnterPin.Text = StringConstants.EnterNewPincode;
				}
			} 
			else if (_pincodePageType == PincodePageType.ExistingPincode || _pincodePageType == PincodePageType.ChangePincodeStep1) {
				MakePinRowActive (gridPinTiles1.PinTiles);

				_pinNumbersFirstRow = new string[5];

				_numberOfPinTiles = 5;

				labelHeaderTitle.Text = "Inloggen";
				labelEnterPin.Text = StringConstants.EnterYourPincode;

				//Hide some UI elements
				labelReEnterPin.IsVisible = false;
				gridPinTiles2.IsVisible = false;
			}
		}

		void ChangeAppearenceBasedOnDeviceType ()
		{
			if (DeviceTypeHelper.IsPhone) {
				labelHeaderTitle.FontSize = 24;
				labelEnterPin.FontSize = 14;
				labelReEnterPin.FontSize = 14;
			} else if (DeviceTypeHelper.IsTablet) {
				labelHeaderTitle.FontSize = 32;
				labelEnterPin.FontSize = 15;
				labelReEnterPin.FontSize = 15;

				layoutNumberPad.Padding = new Thickness (20, 20, 20, 40);

				gridBackground.RowDefinitions [0] = new RowDefinition { Height = 90 };
			}
		}

		void SetColorTheme (ColorTheme appTheme)
		{
			//Set colors
			gridBackground.BackgroundColor = appTheme.ColorPageBackground;
			
			gridPinTiles1.SetColorTheme (appTheme);
			gridPinTiles2.SetColorTheme (appTheme);

			labelHeaderTitle.TextColor = appTheme.ColorPincodePageTitleH1;
			labelEnterPin.TextColor = appTheme.ColorPincodePageTitleH2;
			labelReEnterPin.TextColor = appTheme.ColorPincodePageTitleH2;

			//Re-draw pin tiles to update colors
			ClearEnteredPincode ();
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			ButtonNumberPad.ButtonNumberPadClicked += HandleButtonPinClicked;

			if (_pincodePageType == PincodePageType.ExistingPincode) {
				var doesSupportFingerprint = DependencyService.Get<IFingerprintAuthentication> ().DoesSupportFingerprintAuthentication ();
				if (doesSupportFingerprint) {
					if (!_fingerPrintDialogAlreadyShown) {

						_fingerPrintDialogAlreadyShown = true;
						AuthenticateUsingFingerprint ();
					}
				}
			}
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();

			ButtonNumberPad.ButtonNumberPadClicked -= HandleButtonPinClicked;
		}

		protected override bool OnBackButtonPressed ()
		{
			return true; //disable back button
		}

		async void AuthenticateUsingFingerprint ()
		{
			var authenticationResponse = DependencyService.Get<IFingerprintAuthentication> ().Authenticate ();

			if (authenticationResponse == FingerprintAuthenticationResponse.Accepted) {

				string storedHashedPincode = PincodeValidationHelper.GetHashedPincodeForFingerprintAuthentication ();

				UnlockDatabase (storedHashedPincode);
			}
			else if(authenticationResponse == FingerprintAuthenticationResponse.Failed) {
				await Task.Delay (500); //Fix otherwise the displayalert will not be shown
				await DisplayAlert ("Error", "Er is een fout opgetreden bij het scannen van uw vinger. Voer daarom a.u.b. handmatig uw pincode in.", "OK");
			}
		}

		async void HandleButtonPinClicked (object sender, EventArgs e)
		{
			try {
				if (_activePinTile < _numberOfPinTiles) {
					var clickedNumber = ((ButtonNumberPadClickedEventArgs)e).ClickedNumber;

					if (clickedNumber >= 0 && clickedNumber <= 9) {
						
						if (_activePinTile < 5) { //first pin row
							gridPinTiles1.PinTiles [_activePinTile].ShowStar ();
							_pinNumbersFirstRow [_activePinTile] = clickedNumber.ToString ();
						} else { //second pin row
							gridPinTiles2.PinTiles [_activePinTile - 5].ShowStar ();
							_pinNumbersSecondRow [_activePinTile - 5] = clickedNumber.ToString ();
						}
						_activePinTile++;
							
						if (_activePinTile == 5) {
							MakePinRowActive (gridPinTiles2.PinTiles);
						}
					} else if (clickedNumber == 11) { //backspace clicked
						if (_activePinTile > 0) {

							if (_activePinTile <= 5) { //first pin row
								gridPinTiles1.PinTiles [_activePinTile - 1].SetActive ();
							} else { //second pin row
								gridPinTiles2.PinTiles [_activePinTile - 6].SetActive ();
							}

							if (_activePinTile == 5) {
								MakePinRowInActive (gridPinTiles2.PinTiles);
							}
							_activePinTile--;
						}
					}
					if (_activePinTile == _numberOfPinTiles) { //Pin complete so start validation!
						await ValidatePincode ();
					}
				}
			} catch (Exception ex) {
				Debug.WriteLine (ex.Message);
			}
		}

		async Task ValidatePincode ()
		{
			string enteredPincode1 = "";
			string enteredPincode2 = "";

			if (_pincodePageType == PincodePageType.NewPincode || _pincodePageType == PincodePageType.ChangePincodeStep2) { //New pincode to validate
				foreach (var item in _pinNumbersFirstRow) {
					enteredPincode1 += item;
				}
				foreach (var item in _pinNumbersSecondRow) {
					enteredPincode2 += item;
				}

				if (enteredPincode1.Equals (enteredPincode2)) { //The two pincodes are equal

					await Task.Delay (100); //ensures last "*" is shown before display alert is opened

					//Check for if entered pincode meets complexity requirements
					if (PincodeValidationHelper.IsPincodeComplexEnough (_pinNumbersFirstRow)) {
						//await DisplayAlert ("Success", "Geldige pincode ingesteld", "OK");

						string hashedPincode = PincodeValidationHelper.GetHashedPincodeFromPlainPincode (enteredPincode1);

						if (_pincodePageType == PincodePageType.NewPincode) {
							SetPinForDatabase (hashedPincode);
						}else if(_pincodePageType == PincodePageType.ChangePincodeStep2){
							UpdatePinForDatabase (hashedPincode);
						}
					} else {
						await DisplayAlert ("Error", "Uw pincode is niet complex genoeg", "OK");
						ClearEnteredPincode ();
					}
				} else { // The two pincodes do are not equal
					await DisplayAlert ("Error", "De ingevoerde pincodes komen niet overeen", "OK");
					ClearEnteredPincode ();
				}
			}
			else if (_pincodePageType == PincodePageType.ExistingPincode || _pincodePageType == PincodePageType.ChangePincodeStep1) { //Existing pincode to validate
				foreach (var item in _pinNumbersFirstRow) {
					enteredPincode1 += item;
				}
		
				string hashedPincode = PincodeValidationHelper.GetHashedPincodeFromPlainPincode (enteredPincode1);

				UnlockDatabase (hashedPincode);
			}
		}

		async void SetPinForDatabase (string pincode)
		{
			//Set pincode for local database
			if (DatabaseHelper.Instance.OpenConnection (pincode)) {

				PincodeValidationHelper.SaveAcceptedHashedPincode (pincode);

				ClosePage ();
			} else {
				await DisplayAlert ("Foutmelding", "Er is een fout opgetreden bij het instellen van uw pincode." +
					"\nProbeer het a.u.b. opnieuw", "OK");
				ClearEnteredPincode ();
			}
		}

		async void UpdatePinForDatabase(string newPincode)
		{
			if (DatabaseHelper.Instance.ChangePincode (newPincode)) {
				PincodeValidationHelper.SaveAcceptedHashedPincode (newPincode);

				ClosePage ();
			}
			else {
				await DisplayAlert ("Foutmelding", "Er is een fout opgetreden bij het wijzigen van uw pincode." +
					"\nProbeer het a.u.b. opnieuw", "OK");
				ClearEnteredPincode ();
			}
		}

		async void UnlockDatabase (string hashedPincode) //Check if entered pincode is valid by unlocking the local database
		{ 
			await Task.Delay (100); //ensures last "*" is shown before unlocking database

			if (DatabaseHelper.Instance.OpenConnection (hashedPincode)) { //Correct pincode entered

				PincodeValidationHelper.ResetNumberOfRemainingLoginAttempts ();

				ClosePage ();
			}
			else { //Invalid pin entered

				PincodeValidationHelper.DecreaseNumberOfRemainingLoginAttempts ();

				var remainingPinAttempts = PincodeValidationHelper.GetNumberOfRemainingLoginAttempts ();

				if (remainingPinAttempts < 1) {
					ResetApp ();
				} else {
					await DisplayAlert ("Foutmelding", "Foutieve pincode ingevoerd." +
						"\nU kunt nog " + remainingPinAttempts + " pogingen doen voordat de app wordt reset." +
						"\n\nLET OP: bij een reset zal alle opgeslagen data verloren gaan.", "OK");
					ClearEnteredPincode ();
				}
			}
		}

		void ClearEnteredPincode ()
		{
			MakePinRowActive (gridPinTiles1.PinTiles);
			MakePinRowInActive (gridPinTiles2.PinTiles);

			_activePinTile = 0;
		}

		void MakePinRowActive (List<PinTile> pinTiles)
		{
			foreach (var item in pinTiles) {
				item.SetActive ();
			}
		}

		void MakePinRowInActive (List<PinTile> pinTiles)
		{
			foreach (var item in pinTiles) {
				item.SetInActive ();
			}
		}

		async void OnButtonInfoClicked (object sender, EventArgs args)
		{
			await Navigation.PushModalAsync (new InfoPage ());
		}

		void ClosePage ()
		{
			if (_pincodePageType == PincodePageType.ChangePincodeStep1) {
				App.Current.MainPage = new PincodePage (PincodePageType.ChangePincodeStep2);
			} else {
				App.Current.MainPage = new TabBarPage ();
			}
		}

		void ResetApp ()
		{
			//Delete all local stored files including local database
			FilesystemHelper.DeleteAllFilesAndSettings ();

			//Open reset app page
			App.Current.MainPage = new AppResetPage ();
		}
	}
}