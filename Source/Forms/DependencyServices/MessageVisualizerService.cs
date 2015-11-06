using System.Threading.Tasks;

using Xamarin.Forms;

/// <summary>
/// Message visualizer service.
/// Service locator to show a message somewhere in the app
/// Example how to use:
/// await DependencyService.Get<MessageVisualizerService>().ShowMessage ("Information", "This is an awesome message", "OK");
/// </summary>

namespace MCCForms
{
    public class MessageVisualizerService
    {
        public async Task<bool> ShowMessage (string title, string message, string ok, string cancel=null)
        {
            if (cancel == null) {
				Device.BeginInvokeOnMainThread (async ()=>{
                	await Application.Current.MainPage.DisplayAlert(title, message, ok);
				});
                return true;
            }

            return await Application.Current.MainPage.DisplayAlert(title, message, ok, cancel);
        }

		public async Task<string> ShowActionSheet (string title, string option1, string option2, string cancel=null)
		{
			if (cancel == null) {
				return await Application.Current.MainPage.DisplayActionSheet (title, cancel, null, option1, option2);
			}

			return await Application.Current.MainPage.DisplayActionSheet (title, cancel, null, option1, option2);
		}
    }
}