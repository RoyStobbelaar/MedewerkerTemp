using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace MCCForms
{
	public class RssFeedViewModel : BaseViewModel
	{
		public ICommand RefreshCommand { 
			get {
				return new Command (() => RefreshList ());
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

		RssFeedItem _selectedRssFeed;
		public RssFeedItem SelectedRssFeed {
			get {
				return _selectedRssFeed;
			}
			set {
				if (_selectedRssFeed != value) {
					_selectedRssFeed = value;

					RaisePropertyChanged ();
					HandleItemSelected (_selectedRssFeed);
				}
			}
		}

		public ObservableCollection<RssFeedItem> RssFeeds { get; set; }

		public RssFeedViewModel ()
		{
			RssFeeds = new ObservableCollection<RssFeedItem> ();

			RefreshList ();
		}

		async void RefreshList()
		{
			await GetRssFeed ();
		}

		async Task GetRssFeed()
		{
			RssFeeds.Clear ();

			try{
				if (DependencyService.Get<IReachability> ().IsConnected ()) {

					var allRssFeeds = await HttpHelper.Instance.GetTweakersRssFeed();
					allRssFeeds.ForEach (item => RssFeeds.Add (item));
				}
			}catch (Exception ex){
				Debug.WriteLine (ex.Message);
			}
			IsRefreshing = false;
		}

		async void HandleItemSelected(RssFeedItem selectedRssFeedItem)
		{
			await DependencyService.Get<NavigationService> ().GoToPage(new WebViewPage (selectedRssFeedItem.Title, selectedRssFeedItem.Link));
		}
	}
}