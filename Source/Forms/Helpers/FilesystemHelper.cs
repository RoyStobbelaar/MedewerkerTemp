using System;
using System.IO;
using System.Diagnostics;

using Xamarin.Forms;

namespace MCCForms
{
	public static class FilesystemHelper
	{
		public static string DatabaseFile = Path.Combine (DocumentConstants.DocumentsPath, StorageConstants.DbFileName);

		public static bool FileExists (string filePath)
		{
			return File.Exists (filePath);
		}

		public static void WriteBytes(string filename, byte[] plainData)
		{
			try{
				var pathToWriteFileTo = Path.Combine (DocumentConstants.DocumentsPath, filename);

				//encrypt data
				var encryptedData = EncryptionHelper.EncryptByteArray (plainData, pathToWriteFileTo);

				//write encrypted data to filesystem
				File.WriteAllBytes (pathToWriteFileTo, encryptedData);
			}
			catch (Exception ex) {
				Debug.WriteLine (ex.Message);
			}
		}

		public static byte[] ReadBytes(string filename)
		{
			try{
				string filePath = Path.Combine (DocumentConstants.DocumentsPath, filename);

				//read encrypted data
				var encryptedBytes = File.ReadAllBytes (filePath);

				//decrypt data
				var decryptedBytes = EncryptionHelper.DecryptByteArray (encryptedBytes, filePath);

				//return decrypted data;
				return decryptedBytes;
			} 
			catch (Exception ex) {
				Debug.WriteLine (ex.Message);
				return null;
			}
		}

		public static void DeleteAllFilesAndSettings()
		{
			DirectoryInfo documentsDirectoryInfo = new DirectoryInfo(DocumentConstants.DocumentsPath);

			foreach (FileInfo file in documentsDirectoryInfo.GetFiles())
			{
				file.Delete ();

				Debug.WriteLine (file.Name + " deleted");
			}

			DependencyService.Get<ISecurity> ().DeleteValueFromKeyChain (StorageConstants.SaltStorageKey);
			DependencyService.Get<ISecurity> ().DeleteValueFromKeyChain (StorageConstants.HashedPincodeKey);

			//Delete IVs for filepaths in local db
			DatabaseHelper.Instance.DeleteIVsForFilePaths();
		}

		public static ImageSource ImageSourceFromPath(string fullImagePath){

			if(File.Exists (fullImagePath)){

				var decryptedImageInBytes = FilesystemHelper.ReadBytes (Path.GetFileName (fullImagePath));

				Stream streamImage = new MemoryStream(decryptedImageInBytes);

				return ImageSource.FromStream(() => {
						return streamImage; }
				);

			} else {
				return null;
			}
		}
	}
}