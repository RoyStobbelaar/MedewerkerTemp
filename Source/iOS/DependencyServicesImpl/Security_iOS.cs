using System;
using System.Text;
using System.Diagnostics;
using System.Security.Cryptography;

using Xamarin.Forms;

using MCCForms.iOS;

[assembly: Dependency (typeof(Security_iOS))]

namespace MCCForms.iOS
{
	public class Security_iOS : ISecurity
	{
		const string _generatedPincodeKeyKey = "GeneratedPincodeKeyKey";
		const string _numberOfHashRoundsKey = "NumberOfHashRoundsKey";
		
		public string GetGeneratedDatabasePincode (string plainPincode)
		{
			try{
				string pincode = string.Empty;
				if(!string.IsNullOrEmpty (plainPincode)){
					pincode = plainPincode;
				}

				string vendorId = string.Empty;

				vendorId = GetValueFromKeyChain (_generatedPincodeKeyKey);

				if(string.IsNullOrEmpty (vendorId)){
					
					RijndaelManaged rijndaelCipher = new RijndaelManaged ();
					rijndaelCipher.KeySize = 256;
					rijndaelCipher.BlockSize = 128;

					rijndaelCipher.GenerateKey ();

					vendorId = Convert.ToBase64String (rijndaelCipher.Key);

					SaveValueToKeyChain (_generatedPincodeKeyKey, vendorId);
				}

				string salt = GetValueFromKeyChain (StorageConstants.SaltStorageKey);

				string hashRoundsString = GetValueFromKeyChain (_numberOfHashRoundsKey);
				int hashRounds = 0;

				if (!string.IsNullOrEmpty (hashRoundsString)) {
					int.TryParse (hashRoundsString, out hashRounds);
				}

				if (string.IsNullOrEmpty (salt) || hashRounds < 1) {
					hashRounds = new Random ().Next (3, 11);

					RijndaelManaged rijndaelCipher = new RijndaelManaged ();
					rijndaelCipher.KeySize = 256;
					rijndaelCipher.BlockSize = 128;

					rijndaelCipher.GenerateIV ();

					salt = Convert.ToBase64String (rijndaelCipher.IV);

					SaveValueToKeyChain (StorageConstants.SaltStorageKey, salt);
					SaveValueToKeyChain (_numberOfHashRoundsKey, hashRounds.ToString ());
				}

				SHA1CryptoServiceProvider sha1Provider = new SHA1CryptoServiceProvider ();
				for (int i = 0; i < hashRounds; i++) {

					if (string.IsNullOrEmpty (pincode)) {
						pincode = vendorId;
					}

					pincode += i % 2 == 0 ? salt : string.Empty;

					byte[] originalBytes = Encoding.Default.GetBytes (pincode);
					byte[] encodedBytes = sha1Provider.ComputeHash (originalBytes);

					pincode = string.Empty;

					foreach (byte bit in encodedBytes) {
						pincode += bit.ToString ("x2");
					}
				}

				return pincode;
			}
			catch (Exception ex){
				Debug.WriteLine (ex.Message);

				return null;
			}
		}

		public string GetValueFromKeyChain (string entryKey)
		{
			return KeyChainHelper.GetValueFromKeyChain (entryKey);
		}

		public bool SaveValueToKeyChain (string entryKey, string entryValue)
		{
			return KeyChainHelper.SaveValueToKeyChain (entryKey, entryValue);
		}

		public bool DeleteValueFromKeyChain (string entryKey)
		{
			return KeyChainHelper.DeleteValueFromKeyChain (entryKey);
		}
	}
}