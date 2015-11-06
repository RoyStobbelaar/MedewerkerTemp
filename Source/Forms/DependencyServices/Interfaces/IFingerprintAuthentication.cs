namespace MCCForms
{
	public enum FingerprintAuthenticationResponse
	{
		Accepted,
		Failed,
		Cancelled
	}

	public interface IFingerprintAuthentication
	{
		bool DoesSupportFingerprintAuthentication();

		FingerprintAuthenticationResponse Authenticate();
	}
}