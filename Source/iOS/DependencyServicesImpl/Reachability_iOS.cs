using System;
using System.Net;

using CoreFoundation;
using SystemConfiguration;

using Xamarin.Forms;

using MCCForms.iOS;

[assembly: Dependency (typeof(Reachability_iOS))] //How to use in Forms project: DependencyService.Get<IReachability> ().IsConnected();

namespace MCCForms.iOS
{
	public class Reachability_iOS : IReachability
	{
		public Reachability_iOS ()
		{
		}

		public bool IsConnected ()
		{
			return IsHostReachable ("www.google.com");
		}
			
		bool IsHostReachable(string host)
		{
			if (string.IsNullOrEmpty(host))
				return false;

			using (var r = new NetworkReachability(host))
			{
				NetworkReachabilityFlags flags;

				if (r.TryGetFlags(out flags))
				{
					return IsReachableWithoutRequiringConnection(flags);
				}
			}
			return false;
		}

		bool IsReachableWithoutRequiringConnection(NetworkReachabilityFlags flags)
		{
			// Is it reachable with the current network configuration?
			bool isReachable = (flags & NetworkReachabilityFlags.Reachable) != 0;

			// Do we need a connection to reach it?
			bool noConnectionRequired = (flags & NetworkReachabilityFlags.ConnectionRequired) == 0;

			// Since the network stack will automatically try to get the WAN up,
			// probe that
			if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
				noConnectionRequired = true;

			return isReachable && noConnectionRequired;
		}
	}
}