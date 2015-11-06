using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

/// <summary>
/// Navigation service.
/// Service locator to navigate through pages
/// Example how to use:
/// await DependencyService.Get<NavigationService> ().GoToPage (new MyPage (), true);
/// </summary>

namespace MCCForms
{
	public class NavigationService
	{
		static bool isMasterDetailPageAndTablet = false;

		public static Page CurrentModalPage { 
			get { 
				return CurrentMainPage.Navigation.ModalStack.Count > 0 ? CurrentMainPage.Navigation.ModalStack.LastOrDefault () : null; 
			} 
		}

		public static Page CurrentMainPage { 
			get {
				isMasterDetailPageAndTablet = false;

				if(Application.Current.MainPage is TabbedPage){ //Fix for tabbed page navigation 

					TabbedPage currentMainPage = (TabbedPage)Application.Current.MainPage;

					return currentMainPage.CurrentPage;
				} 
				if(Application.Current.MainPage is MasterDetailPage){ //Fix for masterdetail page navigation 

					MasterDetailPage currentMainPage = (MasterDetailPage)Application.Current.MainPage;

					if (DeviceTypeHelper.IsTablet) {
						isMasterDetailPageAndTablet = true;
						return currentMainPage;
					} else {
						return currentMainPage.Detail;
					}
				}
				else {
					return Application.Current.MainPage;
				}
			}
		}

		public async Task GoToPage (Page page, bool animate = true, bool addToNavigationStackMasterPage = false, bool addToNavigationStackDetailPage = false)
		{
			var currentPage = CurrentMainPage;

			if (isMasterDetailPageAndTablet) {
				MasterDetailPage currentMasterDetailPage = (MasterDetailPage)currentPage;

				if (addToNavigationStackMasterPage) {
					currentMasterDetailPage.Master.Navigation.PushAsync (page, animate);

					//reset detail page to a black page
					var navigationPage = new NavigationPage(new ContentPage());

					if (DeviceTypeHelper.IsAndroid) {
						navigationPage.BarBackgroundColor = AppConfigConstants.AppTheme.ColorToolbarBackground;
					} else {
						navigationPage.BarBackgroundColor = AppConfigConstants.AppTheme.ColorSecondToolbarBackground;
					}
					currentMasterDetailPage.Detail = navigationPage;
				}
				else if (addToNavigationStackDetailPage) {
					currentMasterDetailPage.Detail.Navigation.PushAsync (page, animate);
				} else {
					var navigationPage = new NavigationPage(page);

					if (DeviceTypeHelper.IsAndroid) {
						navigationPage.BarBackgroundColor = AppConfigConstants.AppTheme.ColorToolbarBackground;
					} else {
						navigationPage.BarBackgroundColor = AppConfigConstants.AppTheme.ColorSecondToolbarBackground;
					}
					currentMasterDetailPage.Detail = navigationPage;
				}
			} 
			else {
				await currentPage.Navigation.PushAsync (page, animate);
			}
		}

		public async Task GoToModalPage (Page page, bool animate = true)
		{
			await CurrentMainPage.Navigation.PushModalAsync (page, animate);
		}

		public async Task PopCurrentModalPage (bool animate = true)
		{
			if (CurrentModalPage != null) {
				await CurrentModalPage.Navigation.PopModalAsync (animate);
			}
		}

		public async Task PopCurrentPage (bool animate = true)
		{
			if (CurrentMainPage != null) {
				await CurrentMainPage.Navigation.PopAsync (animate);
			}
		}

		public async Task PopBackgroundEnteredPage ()
		{
			if (CurrentModalPage != null) {
				if(CurrentModalPage.GetType () == typeof(BackgroundEnteredPage))
					await CurrentModalPage.Navigation.PopModalAsync (false);
			}
		}
	}
}