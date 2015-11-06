using System;
using System.Linq;

using Xamarin.Forms;

namespace MCCForms
{
	public static class PincodeValidationHelper
	{
		const string _remainingLoginAttemptsKey = "RemainingLoginAttemptsKey";

		public static bool IsPincodeComplexEnough(string[] pincodeInts)
		{
			bool isComplexEnough = true;
			string pincodeAsString = "";

			foreach (var item in pincodeInts) {
				pincodeAsString += item;
			}

			string numbersInIncreasingOrder = "0123456789";
			string numbersInDecreasingOrder = "9876543210";

			if (numbersInIncreasingOrder.Contains (pincodeAsString)) {  
				isComplexEnough = false;
			} else if (numbersInDecreasingOrder.Contains (pincodeAsString)) {
				isComplexEnough = false;
			} else if (pincodeAsString.Distinct ().Count () == 1) {
				isComplexEnough = false;
			}

			return isComplexEnough;
		}


		public static int GetNumberOfRemainingLoginAttempts()
		{
			try{
				return int.Parse (DependencyService.Get<ISecurity> ().GetValueFromKeyChain (_remainingLoginAttemptsKey));
			} catch{
				return AppConfigConstants.NumberOfLoginAttemptsBeforeAppWillBeReset;
			}
		}

		public static void DecreaseNumberOfRemainingLoginAttempts()
		{
			var existingRemainingLoginAttempts = GetNumberOfRemainingLoginAttempts ();

			DependencyService.Get<ISecurity> ().SaveValueToKeyChain (_remainingLoginAttemptsKey, (existingRemainingLoginAttempts - 1).ToString ());
		}

		public static void ResetNumberOfRemainingLoginAttempts()
		{
			DependencyService.Get<ISecurity> ().SaveValueToKeyChain (_remainingLoginAttemptsKey, (AppConfigConstants.NumberOfLoginAttemptsBeforeAppWillBeReset).ToString ());
		}

		public static string GetHashedPincodeFromPlainPincode(string plainPincode)
		{
			return DependencyService.Get<ISecurity> ().GetGeneratedDatabasePincode (plainPincode);
		}

		public static string GetHashedPincodeForFingerprintAuthentication()
		{
			return DependencyService.Get<ISecurity> ().GetValueFromKeyChain (StorageConstants.HashedPincodeKey);
		}

		public static void SaveAcceptedHashedPincode(string hashedPin)
		{
			DependencyService.Get<ISecurity> ().SaveValueToKeyChain (StorageConstants.HashedPincodeKey, hashedPin);

			ResetNumberOfRemainingLoginAttempts ();
		}
	}
}