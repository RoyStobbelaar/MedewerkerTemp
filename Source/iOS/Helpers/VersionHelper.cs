/// <summary>
/// Version helper.
/// Shows the version number of your app in the iOS settings screen.
/// 
/// HOW TO USE:
/// Add the following line of code in the AppDelegate class within the FinishedLaunching method:
/// 	VersionHelper.AddVersionNumberToIosSettingsScreen();
/// </summary>

using System;

using Foundation;

namespace MCCForms.iOS
{
	public static class VersionHelper
	{
		public static void AddVersionNumberToIosSettingsScreen(){
			Version version = new Version(NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString());
			NSUserDefaults.StandardUserDefaults.SetString (version.ToString(), "version_preference");
		}
	}
}