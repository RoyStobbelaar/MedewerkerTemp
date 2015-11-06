using Xamarin.Forms;

namespace MCCForms
{ 
	public partial class RssFeedPage : ContentPage
	{
		public RssFeedPage ()
		{
			InitializeComponent ();

			BindingContext = new RssFeedViewModel ();
		}
	}
}