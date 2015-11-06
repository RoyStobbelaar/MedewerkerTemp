using Android.Content;

using Xamarin.Forms;

using MCCForms.Droid;

[assembly: Xamarin.Forms.Dependency (typeof (DeviceAndAppInformation_Droid))] //How to use in Forms project: DependencyService.Get<IDeviceAndAppInformation> ().GetOperationSystemVersionNumber();

namespace MCCForms.Droid
{
	public class DeviceAndAppInformation_Droid : IDeviceAndAppInformation
	{
		public string GetOperationSystemVersionNumber ()
		{
			return Android.OS.Build.VERSION.Release;
		}

		public string GetAppVersionNumber ()
		{
			Context context = Forms.Context;
			return context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
		}
	}
}