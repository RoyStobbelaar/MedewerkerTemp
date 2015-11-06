using Xamarin.Forms;

namespace MCCForms
{
	public class DeviceTypeHelper
	{
		public static bool IsTablet {
			get {
				return Device.Idiom == TargetIdiom.Tablet ? true : false;
			}
		}

		public static bool IsPhone {
			get {
				return Device.Idiom == TargetIdiom.Phone ? true : false;
			}
		}

		public static bool IsIos {
			get {
				return Device.OS == TargetPlatform.iOS ? true : false;
			}
		}
			
		public static bool IsAndroid {
			get {
				return Device.OS == TargetPlatform.Android ? true : false; 
			}
		}
	}
}