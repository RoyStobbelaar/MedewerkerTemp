
/// <summary>
/// Storage constants.
/// Constants related to storage and file encryption
/// </summary>

namespace MCCForms
{
	public static class StorageConstants
	{
		public const string DbFileName = "localdb.db";
		public const string DbSettingVersionKey = "DatabaseVersion";

		public const string SaltStorageKey = "SaltStorageKey";
		public const string HashedPincodeKey = "HashedPincodeKey"; //used to decrypt local db when authenticate using fingerprint

		public const string EncryptionKey = "EncryptionKey";

		public const string AccessTokenKey = "AccessToken";
		public const string RefreshTokenKey = "RefreshToken";
		public const string AccessTokenExpirationDateKey = "AccessTokenExpirationDateKey";
		public const string RefreshTokenExpirationDateKey = "RefreshTokenExpirationDateKey";
	}
}