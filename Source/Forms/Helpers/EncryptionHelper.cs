using System;
using System.IO;
using System.Security.Cryptography;

namespace MCCForms
{
	public class EncryptionHelper
	{
		public static byte[] EncryptByteArray(byte[] plainData, string filePath)
		{
			var algorithm = GetAlgorithm(filePath);
			ICryptoTransform transform = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);

			MemoryStream memory = new MemoryStream();
			using (Stream stream = new CryptoStream(memory, transform, CryptoStreamMode.Write))
			{
				stream.Write(plainData, 0, plainData.Length);
			}
			return memory.ToArray();
		}

		public static byte[] DecryptByteArray(byte[] encryptedData, string filePath)
		{
			var algorithm = GetAlgorithm(filePath);
			ICryptoTransform transform = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);

			MemoryStream memory = new MemoryStream();
			using (Stream stream = new CryptoStream(memory, transform, CryptoStreamMode.Write))
			{
				stream.Write(encryptedData, 0, encryptedData.Length);
			}
			return memory.ToArray();
		}
			
		static RijndaelManaged GetAlgorithm(string filePath)
		{
			string iv = DatabaseHelper.Instance.GetIVForFilePath (filePath);

			if (string.IsNullOrEmpty (iv)) {
				var rijndael = new RijndaelManaged ();
				rijndael.KeySize = 256;
				rijndael.BlockSize = 128;
				rijndael.GenerateIV ();
				iv = Convert.ToBase64String (rijndael.IV);

				DatabaseHelper.Instance.AddFilePathIV (new FilePathIV { FilePath = filePath, IV = iv});
			}

			string key;

			Setting keyObject = DatabaseHelper.Instance.GetSetting (StorageConstants.EncryptionKey);

			if (keyObject == null) {
				var rijndael = new RijndaelManaged ();
				rijndael.KeySize = 256;
				rijndael.BlockSize = 128;
				rijndael.GenerateKey ();
				key = Convert.ToBase64String (rijndael.Key);

				DatabaseHelper.Instance.AddSetting (new Setting { Key = StorageConstants.EncryptionKey, Value = key });
			} else {
				key = keyObject.Value;
			}

			var algorithm = new RijndaelManaged ();
			algorithm.KeySize = 256;
			algorithm.BlockSize = 128;

			algorithm.IV = Convert.FromBase64String(iv);
			algorithm.Key = Convert.FromBase64String(key);

			return algorithm;
		}
	}
}