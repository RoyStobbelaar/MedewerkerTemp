using System.Threading.Tasks;

using Xamarin.Media;

using MCCForms.Droid;

using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency (typeof (MediaPicker_Droid))] //How to use in Forms project: DependencyService.Get<IMediaPicker> ().GetPathToPictureFromCamera();

namespace MCCForms.Droid
{
	public class MediaPicker_Droid : IMediaPicker
	{
		MediaPicker _mediaPicker;

		public MediaPicker_Droid ()
		{
			_mediaPicker = new MediaPicker (Forms.Context);
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