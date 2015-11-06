using System;

using MCCForms.Droid;

[assembly: Xamarin.Forms.Dependency (typeof (DocumentsPath_Droid))] //How to use in Forms project: DependencyService.Get<IDocumentsPath> ().GetDocumentsPath();

namespace MCCForms.Droid
{
	public class DocumentsPath_Droid : IDocumentsPath
	{
		public DocumentsPath_Droid()
		{
		}

		public string GetDocumentsPath ()
		{
			return Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
		}
	}
}