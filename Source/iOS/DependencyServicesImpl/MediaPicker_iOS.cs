using System;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Media;

using MCCForms.iOS;

[assembly: Xamarin.Forms.Dependency (typeof (MediaPicker_iOS))] //How to use in Forms project: DependencyService.Get<IMediaPicker> ().GetPathToPictureFromCamera();

namespace MCCForms.iOS
{
	public class MediaPicker_iOS : IMediaPicker
	{
		MediaPicker _mediaPicker;

		public MediaPicker_iOS()
		{
			_mediaPicker = new MediaPicker ();
		}

		public async Task<string> GetPathToPictureFromCamera()
		{
			return await MediaPickerHelper.PathToPictureFromCamera (_mediaPicker);
		}

		public async Task<string> GetPathToPictureFromGallery()
		{
			return await MediaPickerHelper.PathToPictureFromGallery (_mediaPicker);
		}

		public async Task<string> GetPathToVideoFromCamera()
		{
			return await MediaPickerHelper.PathToVideoFromCamera (_mediaPicker);
		}

		public async Task<string> PathToVideoFromGallery ()
		{
			return await  MediaPickerHelper.PathToVideoFromGallery (_mediaPicker);
		}
	}
}