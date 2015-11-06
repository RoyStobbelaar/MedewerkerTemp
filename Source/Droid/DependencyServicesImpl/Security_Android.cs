using System;
using System.Text;
using System.Security.Cryptography;

using Android.Content;

using Xamarin.Forms;

using MCCForms.iOS;

[assembly: Dependency (typeof(Security_Android))]

namespace MCCForms.iOS
{
	public class Security_Android : ISecurity
	{
		const string _keyChainServiceName = "MCCForms";

		//TODO - find a more secure alternative for Shared Preferences
		ISharedPreferences _preferences;
		ISharedPreferencesEditor _preferenceEditor;

		public Security_Android()
		{
			_preferences = Forms.Context.GetSharedPreferences(_keyChainServiceName, FileCreationMode.Private);
			_preferenceEditor = _preferences.Edit();
		}

		public string GetGeneratedDatabasePincode (string plainPincode)
		{
			SHA1CryptoServiceProvider sha1Provider = new SHA1CryptoServiceProvider ();

			string pincode = string.Empty;

			if(!string.IsNullOrEmpty (plainPincode)){
				pincode = plainPincode;
			}

			string key = string.Empty;

			#if DEBUG
			key = "XI134=fV$/IP/ qE@|3a0iI:-R-|<*6C>`!F,Np9O;L1KhT";
			#else
			key +=  "35" +
			Android.OS.Build.Board.Length % 10 + Android.OS.Build.Brand.Length % 10 + 
			Android.OS.Build.CpuAbi.Length % 10 + Android.OS.Build.Device.Length % 10 + 
			Android.OS.Build.Display.Length % 10 + Android.OS.Build.Host.Length % 10 + 
			Android.OS.Build.Id.Length % 10 + Android.OS.Build.Manufacturer.Length % 10 + 
			Android.OS.Build.Model.Length % 10 + Android.OS.Build.Product.Length % 10 + 
			Android.OS.Build.Tags.Length % 10 + Android.OS.Build.Type.Length % 10 + 
			Android.OS.Build.User.Length % 10;
			#endif

			int hashRounds = 0;

			char[] charArray = key.ToCharArray ();
			Array.Reverse (charArray);

			hashRounds = Android.OS.Build.Board.Length % 2 == 0 ? 1 : 2;
			hashRounds += Android.OS.Build.Brand.Length % 2 == 0 ? 1 : 2;
			hashRounds += Android.OS.Build.Model.Length % 2 == 0 ? 1 : 2;
			hashRounds += Android.OS.Build.Device.Length % 2 == 0 ? 1 : 2;
			hashRounds += Android.OS.Build.Product.Length % 2 == 0 ? 1 : 2;

			string salt = Convert.ToBase64String (sha1Provider.ComputeHash (Encoding.Default.GetBytes (new string (charArray))));

			for (int i = 0; i < hashRounds; i++) {

				if (string.IsNullOrEmpty (pincode)) {
					pincode = key;
				}

				pincode += i % 2 == 0 ? salt : string.Empty;

				byte[] originalBytes = Encoding.Default.GetBytes (pincode);
				byte[] encodedBytes = sha1Provider.ComputeHash (originalBytes);

				pincode = string.Empty;

				foreach (byte bit in encodedBytes) {
					pincode += bit.ToString ("x2");
				}
			}

			SaveValueToKeyChain (StorageConstants.SaltStorageKey, salt);

			return pincode;
		}

		public string GetValueFromKeyChain (string entryKey)
		{
			var returnValue = _preferences.GetString (entryKey, null);

			return returnValue; 
		}

		public bool SaveValueToKeyChain (string entryKey, string entryValue)
		{
			try{
				_preferenceEditor.PutString (entryKey, entryValue);
				_preferenceEditor.Commit();

				return true;
			} 
			catch {
				return false;
			}
		}

		public bool DeleteValueFromKeyChain (string entryKey)
		{
			try{
				_preferenceEditor.Remove (entryKey);
				_preferenceEditor.Commit();

				return true;
			} 
			catch {
				return false;
			}
		}
	}
}