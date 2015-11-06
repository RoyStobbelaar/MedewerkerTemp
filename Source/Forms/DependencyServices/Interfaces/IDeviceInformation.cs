using System;

namespace MCCForms
{
	public interface IDeviceAndAppInformation
	{
		string GetOperationSystemVersionNumber();

		string GetAppVersionNumber();
	}
}