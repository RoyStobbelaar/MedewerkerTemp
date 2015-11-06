using UIKit;
using Foundation;

using MCCForms.iOS;

[assembly: Xamarin.Forms.Dependency (typeof (DeviceAndAppInformation_iOS))] //How to use in Forms project: DependencyService.Get<IDeviceAndAppInformation> ().GetOperationSystemVersionNumber();

namespace MCCForms.iOS
{
	public class DeviceAndAppInformation_iOS : IDeviceAndAppInformation
	{
		public string GetOperationSystemVersionNumber ()
		{
			return UIDevice.CurrentDevice.SystemVersion;
		}

		public string GetAppVersionNumber ()
		{
			return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
		}
	}
}