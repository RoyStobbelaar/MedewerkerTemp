using System.Threading;
using System.Diagnostics;

using UIKit;
using Foundation;
using LocalAuthentication;

using MCCForms.iOS;

[assembly: Xamarin.Forms.Dependency (typeof (FingerprintAuthentication_iOS))] //How to use in Forms project: DependencyService.Get<IFingerprintAuthentication> ().DoesSupportFingerprintAuthentication();

namespace MCCForms.iOS
{
	public class FingerprintAuthentication_iOS : IFingerprintAuthentication
	{
		ManualResetEvent _reachedCallBackEvent;
		LAContext _context;

		public FingerprintAuthentication_iOS()
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				_context = new LAContext ();
			}
		}

		public bool DoesSupportFingerprintAuthentication ()
		{
			NSError authError;
			if (_context != null) {
				if (_context.CanEvaluatePolicy (LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out authError)) {
					return true;
				} else {
					return false;
				}
			} else {
				return false;
			}
		}

		public FingerprintAuthenticationResponse Authenticate ()
		{
			_reachedCallBackEvent = new ManualResetEvent(false);

			FingerprintAuthenticationResponse response = FingerprintAuthenticationResponse.Failed;
			
			LAContextReplyHandler replyHandler = new LAContextReplyHandler((success, error) => {
				if(success){
					Debug.WriteLine("Successfully authenticated using TouchID");
					response = FingerprintAuthenticationResponse.Accepted;
				}
				else{
					if(error.Code == -2){ //-2 is error code when cancelled by user
						Debug.WriteLine ("User cancelled to authenticate using TouchID");
						response = FingerprintAuthenticationResponse.Cancelled;
					}
					else {
						Debug.WriteLine ("Failed to authenticate using TouchID");
						response = FingerprintAuthenticationResponse.Failed;
					}
				}
				_reachedCallBackEvent.Set();
			});

			//Use evaluatePolicy to start authentication operation and show the UI as an Alert view
			//Use the LocalAuthentication Policy DeviceOwnerAuthenticationWithBiometrics
			_context.EvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, new NSString("Leg uw vinger op de vingerafdruk scanner om uzelf te authenticeren"), replyHandler);

			_reachedCallBackEvent.WaitOne();

			return response;
		}
	}
}