using Xamarin.Forms;

namespace MCCForms
{
	public partial class WebViewPage : ContentPage
	{
		public WebViewPage (string title, string url)
		{
			InitializeComponent ();

			Title = title;

			webView.Source = url;
		}
	}
}