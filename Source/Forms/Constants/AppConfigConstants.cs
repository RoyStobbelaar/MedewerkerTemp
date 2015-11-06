namespace MCCForms
{
	public static class AppConfigConstants
	{
		public const string PiwikBaseUrl = "https://bdmcc.piwik.prisma-it.com";
		public const int PiwikSiteId = 999; //TODO: update to correct site id

		public const string XamarinInsightsApiKey = ""; //TODO: update to correct API Key (https://insights.xamarin.com/addapp) Docs: https://insights.xamarin.com/docs

		public const string IdentityServerTokenUrl = "https://api-ota.belastingdienst.nl/mcr-ont/core/connect/token"; //TODO: update to correct url
		public const string IdentityServerUserInfoUrl = "https://api-ota.belastingdienst.nl/mcr-ont/core/connect/userinfo"; //TODO: update to correct url
		public const string IdentityServerClientId = "applicatie1_client"; //TODO: update to correct client id
		public const string IdentityServerClientSecret = "4E8D1BB7-41D4-4B35-BF96-3BDF69F83038"; //TODO: update to correct client secret
		public const string IdentityServerClientScope = "applicatie1api offline_access openid";

		public static ColorTheme AppTheme = ColorThemes.ThemeBelastingdienst;
		
		public const bool IsPincodeRequired = true;
		public const int NumberOfLoginAttemptsBeforeAppWillBeReset = 5;

		public const bool HideContentWhenAppIsInBackground = false;

		public const int MinutesBeforeLockingApp = 15;

		public const bool IsCertificatePinningEnabled = false;

		//TODO: update PEM to correct root certificate
		public const string RootPEMForCertificatePinning = 
			"MIID8DCCAtigAwIBAgIDAjp2MA0GCSqGSIb3DQEBBQUAMEIxCzAJBgNVBA" +
			"YTAlVTMRYwFAYDVQQK\r\nEw1HZW9UcnVzdCBJbmMuMRswGQYDVQQDExJH" +
			"ZW9UcnVzdCBHbG9iYWwgQ0EwHhcNMTMwNDA1MTUx\r\nNTU1WhcNMTYxMjM" +
			"xMjM1OTU5WjBJMQswCQYDVQQGEwJVUzETMBEGA1UEChMKR29vZ2xlIEluYz" +
			"El\r\nMCMGA1UEAxMcR29vZ2xlIEludGVybmV0IEF1dGhvcml0eSBHMjCCA" +
			"SIwDQYJKoZIhvcNAQEBBQAD\r\nggEPADCCAQoCggEBAJwqBHdc2FCROgaj" +
			"guDYUEi8iT/xGXAaiEZ+4I/F8YnOIe5a/mENtzJEiaB0\r\nC1NPVaTOgmK" +
			"V7utZX8bhBYASxF6UP7xbSDj0U/ck5vuR6RXEz/RTDfRK/J9U3n2+oGtvh8" +
			"DQUB8o\r\nMANA2ghzUWx//zo8pzcGjr1LEQTrfSTe5vn8MXH7lNVg8y5Kr" +
			"0LSy+rEahqyzFPdFUuLH8gZYR/N\r\nnag+YyuENWllhMgZxUYi+FOVvuOA" +
			"ShDGKuy6lyARxzmZEASg8GF6lSWMTlJ14rbtCMoU/M4iarNO\r\nz0YDl5c" +
			"DfsCx3nuvRTPPuj5xt970JSXCDTWJnZ37DhF5iR43xa+OcmkCAwEAAaOB5z" +
			"CB5DAfBgNV\r\nHSMEGDAWgBTAephojYn7qwVkDBF9qn1luMrMTjAdBgNVH" +
			"Q4EFgQUSt0GFhu89mi1dvWBtrtiGrpa\r\ngS8wEgYDVR0TAQH/BAgwBgEB/" +
			"wIBADAOBgNVHQ8BAf8EBAMCAQYwNQYDVR0fBC4wLDAqoCigJoYk\r\naHR0c" +
			"DovL2cuc3ltY2IuY29tL2NybHMvZ3RnbG9iYWwuY3JsMC4GCCsGAQUFBwEBB" +
			"CIwIDAeBggr\r\nBgEFBQcwAYYSaHR0cDovL2cuc3ltY2QuY29tMBcGA1UdI" +
			"AQQMA4wDAYKKwYBBAHWeQIFATANBgkq\r\nhkiG9w0BAQUFAAOCAQEAJ4zP6" +
			"cc7vsBv6JaE+5xcXZDkd9uLMmCbZdiFJrW6nx7eZE4fxsggWwmf\r\nq6ngC" +
			"TRFomUlNz1/Wm8gzPn68R2PEAwCOsTJAXaWvpv5Fdg50cUDR3a4iowx1mDV5" +
			"I/b+jzG1Zgo\r\n+ByPF5E0y8tSetH7OiDk4Yax2BgPvtaHZI3FCiVCUe+yOL" +
			"jgHdDh/Ob0r0a678C/xbQF9ZR1DP6i\r\nvgK66oZb+TWzZvXFjYWhGiN3Ghk" +
			"XVBNgnwvhtJwoKvmuAjRtJZOcgqgXe/GFsNMPWOH7sf6coaPo\r\n/ck/9Ndx" +
			"3L2MpBngISMjVROPpBYCCX65r+7bU2S9cS+5Oc4wt7S8VOBHBw==";
	}
}