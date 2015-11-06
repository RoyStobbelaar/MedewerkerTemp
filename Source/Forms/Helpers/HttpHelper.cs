using System.Net.Http;

namespace MCCForms
{
	public partial class HttpHelper
	{
		static HttpHelper _instance;

		HttpClient _httpClient;
	
		private HttpHelper(){
			_httpClient = new HttpClient (new ModernHttpClient.NativeMessageHandler(false, AppConfigConstants.IsCertificatePinningEnabled));
		}

		public static HttpHelper Instance {
			get {
				if (_instance == null) {
					_instance = new HttpHelper ();

				}
				return _instance;
			}
		}


		//TODO: For OpenID Connect Application requests you have to add the access token to the request
		//httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Bearer", accessToken);
	}
}