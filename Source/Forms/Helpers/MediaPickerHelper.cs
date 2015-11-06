using System.Threading.Tasks;

using Xamarin.Media;

namespace MCCForms
{
	public static class MediaPickerHelper
	{
		public static async Task<string> PathToPictureFromCamera (MediaPicker mediaPicker)
		{
			try{
				if (!mediaPicker.IsCameraAvailable || !mediaPicker.PhotosSupported) {
					return null;
				}

				var mediaFile = await mediaPicker.TakePhotoAsync (new StoreCameraMediaOptions {});

				return mediaFile.Path;
			} catch {
				return null;
			}
		}

		public static async Task<string> PathToPictureFromGallery (MediaPicker mediaPicker)
		{
			try{
				if (!mediaPicker.PhotosSupported) {
					return null;
				}
				var mediaFile = await mediaPicker.PickPhotoAsync ();

				return mediaFile.Path;
			} catch {
				return null;
			}
		}

		public static async Task<string> PathToVideoFromCamera (MediaPicker mediaPicker)
		{
			try{
				if (!mediaPicker.IsCameraAvailable || !mediaPicker.VideosSupported) {
					return null;
				}

				var mediaFile = await mediaPicker.TakeVideoAsync (new StoreVideoOptions {});

				return mediaFile.Path;
			} catch {
				return null;
			}
		}

		public static async Task<string> PathToVideoFromGallery (MediaPicker mediaPicker)
		{
			try{
				if (!mediaPicker.PhotosSupported) {
					return null;
				}
				var mediaFile = await mediaPicker.PickVideoAsync ();

				return mediaFile.Path;
			}catch {
				return null;
			}
		}
	}
}