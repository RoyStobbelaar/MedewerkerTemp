using Security;
using Foundation;

namespace MCCForms.iOS
{
	public static class KeyChainHelper
	{
		const string _keyChainServiceName = "MCCForms";

		public static bool SaveValueToKeyChain (string entryKey, string entryValue)
		{
			SecRecord record = new SecRecord(SecKind.GenericPassword) { 
				Account = entryKey, 
				Label = entryKey, 
				Service = _keyChainServiceName };

			SecStatusCode resultCode;
			SecKeyChain.QueryAsRecord(record, out resultCode);

			if (resultCode == SecStatusCode.Success) {
				if (SecKeyChain.Remove (record) != SecStatusCode.Success) {
					return false;
				}
			}

			resultCode = SecKeyChain.Add(new SecRecord(SecKind.GenericPassword) { 
				Label = entryKey, 
				Account = entryKey, 
				Service = _keyChainServiceName,
				Accessible = SecAccessible.WhenUnlockedThisDeviceOnly,
				Synchronizable = false,
				ValueData = NSData.FromString(entryValue, NSStringEncoding.UTF8)
			});

			return resultCode == SecStatusCode.Success;
		}

		public static string GetValueFromKeyChain(string entryKey)
		{
			string result = string.Empty;

			SecRecord record = new SecRecord(SecKind.GenericPassword) { 
				Account = entryKey, 
				Label = entryKey, 
				Service = _keyChainServiceName };

			SecStatusCode resultCode;
			SecRecord data = SecKeyChain.QueryAsRecord(record, out resultCode);

			if (resultCode == SecStatusCode.Success) {
				result = NSString.FromData(data.ValueData, NSStringEncoding.UTF8);
			}

			return result;
		}

		public static bool DeleteValueFromKeyChain (string entryKey)
		{
			SecRecord record = new SecRecord(SecKind.GenericPassword) { 
				Account = entryKey, 
				Label = entryKey, 
				Service = _keyChainServiceName 
			};

			SecStatusCode resultCode;
			SecKeyChain.QueryAsRecord(record, out resultCode);

			if (resultCode == SecStatusCode.Success) {
				if (SecKeyChain.Remove (record) != SecStatusCode.Success) {
					return false;
				}
			}

			return true;
		}
	}
}