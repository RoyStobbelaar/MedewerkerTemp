using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using UIKit;
using Foundation;

using MCCForms.iOS;

[assembly: Xamarin.Forms.Dependency (typeof (DocumentsPath_iOS))] //How to use in Forms project: DependencyService.Get<IDocumentsPath> ().GetDocumentsPath();

namespace MCCForms.iOS
{
	public class DocumentsPath_iOS : IDocumentsPath
	{
		public DocumentsPath_iOS()
		{
		}
	
		public string GetDocumentsPath ()
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) { //iOS8 specific code

				var paths = NSFileManager.DefaultManager.GetUrls (NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User);
				var documentsPath =  paths[paths.Length -1].Path; //documents path is last object in list (https://developer.apple.com/library/ios/technotes/tn2406/_index.html#//apple_ref/doc/uid/DTS40014932-CH1-LOCCOM)

				return Path.Combine (documentsPath, "..", "Library");

			} else {// Code to support earlier iOS versions
				var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
				return Path.Combine (documents, "..", "Library");
			}
		}
	}
}