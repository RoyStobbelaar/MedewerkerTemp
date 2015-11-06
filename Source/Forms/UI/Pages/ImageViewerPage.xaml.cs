using System;

using Xamarin.Forms;
using System.IO;

namespace MCCForms
{
	public partial class ImageViewerPage : ContentPage
	{
		public ImageViewerPage (string title, string pathToLocalImage)
		{
			InitializeComponent ();

			Title = title;

			if (File.Exists (pathToLocalImage)) {
				imageView.Source = FilesystemHelper.ImageSourceFromPath (pathToLocalImage);
			} else {
				DisplayAlert ("Foutmelding", "De afbeelding kon niet geopend worden.", null);
			}
		}
	}
}