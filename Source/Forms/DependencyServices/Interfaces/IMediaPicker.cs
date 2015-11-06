using System;
using System.Threading.Tasks;

namespace MCCForms
{
	public interface IMediaPicker
	{
		Task<string> GetPathToPictureFromCamera();

		Task<string> GetPathToPictureFromGallery();

		Task<string> GetPathToVideoFromCamera();

		Task<string> PathToVideoFromGallery ();
	}
}