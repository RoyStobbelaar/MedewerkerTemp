using System.Linq;
using System.Diagnostics;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using MCCForms.Models;

namespace MCCForms
{
	public class ItemListViewModel : BaseViewModel
	{
		public ICommand RefreshCommand { 
			get {
				return new Command (() => RefreshList ());
			}
		}

		public ICommand OpenItemEditPageCommand { 
			get {
				return new Command (() => OpenItemEditPage ());
			}
		}

		public ICommand OpenSettingsPageCommand { 
			get {
				return new Command (() => OpenSettingsPage ());
			}
		}

		bool _isRefreshing;
		public bool IsRefreshing {
			get {
				return _isRefreshing;
			}
			set {
				if (_isRefreshing != value) {
					_isRefreshing = value;
					RaisePropertyChanged ();
				}
			}
		}

		/*
		string _searchText;
		public string SearchText {
			get {
				return _searchText;
			}
			set {
				if (_searchText != value) {
					_searchText = value;

					RaisePropertyChanged ();
					HandleSearchTextChanged (value);
				}
			}
		}
		*/

		Appointment _selectedAppointment;
		public Appointment SelectedAppointment {
			get {
				return _selectedAppointment;
			}
			set {
				if (_selectedAppointment != value) {
					_selectedAppointment = value;

					RaisePropertyChanged ();
					HandleItemSelected (_selectedAppointment);
				}
			}
		}

		public ObservableCollection<Appointment>appointments{ get; set; }
		List<Appointment>_unfilteredAppointments{get;set;}
		List<Appointment>_appointmentsToShow {
			get {
				return _unfilteredAppointments;
			}
			set {
				if (appointments == null) {
					appointments = new ObservableCollection<Appointment> ();
				}
				_unfilteredAppointments = value;
				appointments.Clear ();
				if (_unfilteredAppointments != null) {
					_unfilteredAppointments.ForEach (item => appointments.Add (item));
				}
			}
		}

		//public ObservableCollection<PasswordObject> PasswordItems { get; set; }

		//List<PasswordObject> _unfilteredPasswordItems { get; set; } //used to filter list
					/*

		List<PasswordObject> _passwordItemsToShow { //used to filter list
			get {
				return _unfilteredPasswordItems;
			}
			set {
				if(PasswordItems == null){
					PasswordItems = new ObservableCollection<PasswordObject>();
				}

				_unfilteredPasswordItems = value;

				//add items to observable collection (the collection which is shown in the UI)
				PasswordItems.Clear ();
				if (_unfilteredPasswordItems != null) {
					_unfilteredPasswordItems.ForEach (item => PasswordItems.Add (item));
				}
			}
		}
		*/

		public ItemListViewModel () 
		{
			_appointmentsToShow = DatabaseHelper.Instance.GetAppointments ();
		}

		void RefreshList()
		{
			_appointmentsToShow = DatabaseHelper.Instance.GetAppointments ();

			IsRefreshing = false;
		}

		async void OpenItemEditPage()
		{
			await DependencyService.Get<NavigationService> ().GoToPage(new ItemEditPage(null));
		}

		async void OpenSettingsPage()
		{
			await DependencyService.Get<NavigationService> ().GoToPage(new SettingsPage());
		}
		/*
		void HandleSearchTextChanged (string searchText)
		{
			Debug.WriteLine ("Searched for: " + searchText);

			if (!string.IsNullOrEmpty (searchText)) {

				var searchResults = _appointmentsToShow.Where (t => t.Name.ToLower ().Contains (searchText.ToLower ())).ToList (); 
					//|| t.Password.ToLower ().Contains (searchText.ToLower ())).ToList ();

				//before updating list, first check if list is changed
				var listIsNotChanged = searchResults.SequenceEqual(appointments.ToList ());

				if(!listIsNotChanged){
					
					PasswordItems.Clear();
					searchResults.ForEach (t => PasswordItems.Add (t));
				}
			} 
			else {
				//search bar cancel button clicked - so refresh list
				PasswordItems.Clear();
				_passwordItemsToShow = DatabaseHelper.Instance.GetPasswordObjects ();
			}
		}
		*/
		async void HandleItemSelected(Appointment selectedAppointment)
		{
			await DependencyService.Get<NavigationService> ().GoToPage(new ItemEditPage(selectedAppointment), true);
		}
	}
}