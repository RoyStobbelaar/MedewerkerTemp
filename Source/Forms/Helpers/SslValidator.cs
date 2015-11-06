using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace MCCForms
{
	//How to use:
	//SslValidator sslValidator = new SslValidator ();
	//ServicePointManager.ServerCertificateValidationCallback = sslValidator.ValidateServerCertficate;
	public class SslValidator
	{
		public event EventHandler CertificateMismatchFound;

		X509Certificate2 originalRootCertificate;

		public SslValidator ()
		{
			originalRootCertificate = GetCertFromPEM();
		}

		X509Certificate2 GetCertFromPEM()
		{
			var bytesRootCertificate = Convert.FromBase64String((AppConfigConstants.RootPEMForCertificatePinning).Replace("\n", ""));

			return new X509Certificate2 (bytesRootCertificate);
		}

		public bool ValidateServerCertficate (object sender, X509Certificate receivedCertificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			bool validRequest = true;

//			var enumerator = chain.ChainPolicy.ExtraStore.GetEnumerator();
//
//			while (enumerator.MoveNext ()) {
//				var pem = ExportToPEM (loop.Current);
//			}
//
			if (receivedCertificate.Subject.IndexOf (".xamarin.com", 0, StringComparison.CurrentCultureIgnoreCase) == -1) { //not a request to an Xamarin server so verify certificate
				//This is not an https request for Xamarin Insights

				if (originalRootCertificate == null) {
					validRequest = false;
				} else {
					//check if certificate chain contains original root certificate
					validRequest = chain.ChainPolicy.ExtraStore.Contains (originalRootCertificate);
				}
			}

			if (!validRequest) {
				EventHandler handler = CertificateMismatchFound;
				if (handler != null) {
					handler (this, null);
				}
			}

			return validRequest;
		}

//		static string ExportToPEM(X509Certificate cert)
//		{
//			System.Text.StringBuilder builder = new System.Text.StringBuilder();            
//
//			builder.AppendLine("-----BEGIN CERTIFICATE-----");
//			builder.AppendLine(Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks));
//			builder.AppendLine("-----END CERTIFICATE-----");
//
//			return builder.ToString();
//		}
	}
}