using MCCForms.Droid;

[assembly: Xamarin.Forms.Dependency (typeof (FingerprintAuthentication_Droid))] //How to use in Forms project: DependencyService.Get<IFingerprintAuthentication> ().DoesSupportFingerprintAuthentication();

namespace MCCForms.Droid
{
	public class FingerprintAuthentication_Droid : IFingerprintAuthentication
	{
		public FingerprintAuthentication_Droid ()
		{
		}

		public bool DoesSupportFingerprintAuthentication ()
		{
			return false;
		}

		public FingerprintAuthenticationResponse Authenticate ()
		{
			return FingerprintAuthenticationResponse.Cancelled;
		}
	}
}