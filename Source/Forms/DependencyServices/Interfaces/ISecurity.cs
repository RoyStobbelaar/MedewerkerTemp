using System;

namespace MCCForms
{
	public interface ISecurity
	{
		string GetGeneratedDatabasePincode(string plainPincode);

		string GetValueFromKeyChain(string entryKey);

		bool SaveValueToKeyChain (string entryKey, string entryValue);

		bool DeleteValueFromKeyChain (string entryKey);
	}
}