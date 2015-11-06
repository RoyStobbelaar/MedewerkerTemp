using Xamarin.Forms;
using MCCForms.Models;

namespace MCCForms
{
	public partial class ItemListPage : ContentPage
	{
		bool _isAlreadyShown = false;

		public ItemListPage ()
		{
			InitializeComponent ();

			BindingContext = new ItemListViewModel ();
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			if (_isAlreadyShown) {
				itemListView.BeginRefresh ();
			}
			_isAlreadyShown = true;
		}

		public void OnDeleteItemClick (object sender, System.EventArgs e) { //TODO: find solution to let the view model call the delete operation
			var clickedMenuItem = ((MenuItem)sender);
			var objectToDelete = (Appointment)clickedMenuItem.BindingContext;

			DatabaseHelper.Instance.DeleteAppointmentObject (objectToDelete);
			itemListView.BeginRefresh ();
		}
	}
}