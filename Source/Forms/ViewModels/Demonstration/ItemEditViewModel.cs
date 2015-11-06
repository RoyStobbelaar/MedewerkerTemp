using System;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using MCCForms.Models;

using Xamarin.Forms;

namespace MCCForms
{
	public class ItemEditViewModel : BaseViewModel
	{
		public ICommand SaveCommand { 
			get {
				return new Command (() => SaveItem ());
			}
		}
			/*
		public ICommand SelectLocationCommand { 
			get {
				return new Command (() => SelectLocation ());
			}
		}
		*/

		//Appointment info

		bool _isNewAppointmentObject;
		Appointment _appointmentObject;

		public string Location {
			get {
				return _appointmentObject.Locatie;
			}
			set {
				if (_appointmentObject.Locatie != value) {
					_appointmentObject.Locatie = value;
					RaisePropertyChanged ();
				}
			}
		}

		public string Comment {
			get {
				return _appointmentObject.Comment;
			}
			set {
				if (_appointmentObject.Comment != value) {
					_appointmentObject.Comment = value;
					RaisePropertyChanged ();
				}
			}
		}

		public DateTime Date {
			get {
				return _appointmentObject.Date;
			}
			set {
				if (_appointmentObject.Date != value) {
					_appointmentObject.Date = value;
					RaisePropertyChanged ();
				}
			}
		}

		//Visitor info

		//TODO: List
		public string VisitorFirstName {
			get {
				return _appointmentObject.VisitorList [0].FirstName;
			}
			set {
				if (_appointmentObject.VisitorList [0].FirstName != value) {
					_appointmentObject.VisitorList [0].FirstName = value;
					RaisePropertyChanged ();
				}
			}
		}

		public string VisitorLastName {
			get {
				return _appointmentObject.VisitorList [0].LastName;
			}
			set {
				if (_appointmentObject.VisitorList [0].LastName != value) {
					_appointmentObject.VisitorList [0].LastName = value;
					RaisePropertyChanged ();
				}
			}
		}

		public string VisitorEmail {
			get {
				return _appointmentObject.VisitorList [0].Email;
			}
			set {
				if (_appointmentObject.VisitorList [0].Email != value) {
					_appointmentObject.VisitorList [0].Email = value;
					RaisePropertyChanged ();
				}
			}
		}

		public int VisitorPhone {
			get {
				return _appointmentObject.VisitorList [0].PhoneNumber;
			}
			set {
				if (_appointmentObject.VisitorList [0].PhoneNumber != value) {
					_appointmentObject.VisitorList [0].PhoneNumber = value;
					RaisePropertyChanged ();
				}
			}
		}

		public string VisitorCompany {
			get {
				return _appointmentObject.VisitorList [0].Company;
			}
			set {
				if (_appointmentObject.VisitorList [0].Company != value) {
					_appointmentObject.VisitorList [0].Company = value;
					RaisePropertyChanged ();
				}
			}
		}

		/*
		public string LocationAddress {
			get {
				return _passwordObject.LocationAddress;
			}
			set {
				if (_passwordObject.LocationAddress != value) {
					_passwordObject.LocationAddress = value;
					RaisePropertyChanged ();
				}
			}
		}

		public string LocationZipAndCity {
			get {
				return _passwordObject.LocationZipAndCity;
			}
			set {
				if (_passwordObject.LocationZipAndCity != value) {
					_passwordObject.LocationZipAndCity = value;
					RaisePropertyChanged ();
				}
			}
		}

		public bool IsActive {
			get {
				return _passwordObject.IsActive;
			}
			set {
				if (_passwordObject.IsActive != value) {
					_passwordObject.IsActive = value;
					RaisePropertyChanged ();
				}
			}
		}

		public double ImportanceLevel {
			get {
				return _passwordObject.ImportanceLevel;
			}
			set {
				if (_passwordObject.ImportanceLevel != value) {
					_passwordObject.ImportanceLevel = value;
					RaisePropertyChanged ();
				}
			}
		}

		public DateTime AccountSince {
			get {
				return _passwordObject.AccountSince;
			}
			set {
				if (_passwordObject.AccountSince != value) {
					_passwordObject.AccountSince = value;
					RaisePropertyChanged ();
				}
			}
		}

		public string PathToImage1 {
			get {
				return _passwordObject.PathToImage1;
			}
			set {
				if (_passwordObject.PathToImage1 != value) {
					_passwordObject.PathToImage1 = value;
					RaisePropertyChanged ();
				}
			}
		}

		public string PathToImage2 {
			get {
				return _passwordObject.PathToImage2;
			}
			set {
				if (_passwordObject.PathToImage2 != value) {
					_passwordObject.PathToImage2 = value;
					RaisePropertyChanged ();
				}
			}
		}

		public string PathToImage3 {
			get {
				return _passwordObject.PathToImage3;
			}
			set {
				if (_passwordObject.PathToImage3 != value) {
					_passwordObject.PathToImage3 = value;
					RaisePropertyChanged ();
				}
			}
		}


		public string Category {
			get {
				return _passwordObject.Category;
			}
			set {
				if (_passwordObject.Category != value) {
					_passwordObject.Category = value;
					RaisePropertyChanged ();
				}
			}
		}

		public List<string> CategoriesToChoose{
			get {
				return new List<string> { "Privé", "Zakelijk" };
			}
		}
		*/
		public ItemEditViewModel (Appointment existingAppointmentObject)
		{
			
			if (existingAppointmentObject != null) {
				_appointmentObject = existingAppointmentObject;

				_isNewAppointmentObject = false;
			} else {
				_appointmentObject = new Appointment ();
				_isNewAppointmentObject = true;
			}
		}

		async Task<bool> IsAppointmentObjectValidToSave()
		{
			int numberOfErrors = 0;
			string errorMessage = "De volgende vereiste velden ontbreken:";

			if (string.IsNullOrEmpty (_appointmentObject.Locatie)){
				numberOfErrors++;
				errorMessage += "\n- Wachtwoord voor";
			}

			if (numberOfErrors == 0) {
				return true;
			} else {
				await DependencyService.Get<MessageVisualizerService>().ShowMessage ("Foutmelding", errorMessage, "OK");

				return false;
			}
		}

			/*
		async Task<bool> IsPasswordObjectValidToSave()
		{
			int numberOfErrors = 0;
			string errorMessage = "De volgende vereiste velden ontbreken:";

			if (string.IsNullOrEmpty (_appointmentObject.Locatie)){
				numberOfErrors++;
				errorMessage += "\n- Wachtwoord voor";
			}

			if (numberOfErrors == 0) {
				return true;
			} else {
				await DependencyService.Get<MessageVisualizerService>().ShowMessage ("Foutmelding", errorMessage, "OK");

				return false;
			}
		}
		*/
		
		/*
		async void SelectLocation()
		{
			var locationPage = new LocationDeterminationPage("Uw locatie");

			//listen to location changes from location determination page
			locationPage.LocationChangedEvent += (object sender, EventArgs e) => 
			{
				var retrievedLocation = (LocationChangedEventArgs)e;
				this.LocationAddress = retrievedLocation.Address;
				this.LocationZipAndCity = retrievedLocation.ZipcodeAndCity;
			};

			await DependencyService.Get<NavigationService> ().GoToPage (locationPage);
		}
		*/
		async void SaveItem()
		{
			if(await IsAppointmentObjectValidToSave ()){

				if(_isNewAppointmentObject){//new appointment object
					DatabaseHelper.Instance.InsertAppointmentObject (_appointmentObject);
				} 
				else { //existing password object
					DatabaseHelper.Instance.UpdateAppointmentObject (_appointmentObject);
				}

				await DependencyService.Get<NavigationService> ().PopCurrentPage ();
			} 
		}
	}
}