using System;

using Android.Net;

using Xamarin.Forms;

using MCCForms.Droid;

[assembly: Dependency (typeof (Reachability_Droid))] //How to use in Forms project: DependencyService.Get<IReachability> ().IsConnected();

namespace MCCForms.Droid
{
	public class Reachability_Droid : IReachability
	{
		public Reachability_Droid ()
		{
		}

		public bool IsConnected()
		{
			var connectivityManager = (ConnectivityManager)Forms.Context.GetSystemService(Android.App.Application.ConnectivityService);
			Boolean connection = 	
				connectivityManager.ActiveNetworkInfo != null && 
				connectivityManager.ActiveNetworkInfo.IsAvailable == true && 
				connectivityManager.ActiveNetworkInfo.IsConnected == true;
			connectivityManager.Dispose ();
			return connection;
		}
	}
}