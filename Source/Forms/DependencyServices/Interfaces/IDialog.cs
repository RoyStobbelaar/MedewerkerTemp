using System;

namespace MCCForms
{
	public interface IDialog
	{
		void ShowProgressDialog(string message);

		void HideProgressDialog();
	}
}