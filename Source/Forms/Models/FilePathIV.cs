using SQLite;

/// <summary>
/// File path IV.
/// Object used to write unique encryption IV for specific FilePath to local database
/// </summary>

namespace MCCForms
{
	public class FilePathIV
	{
		[PrimaryKey]
		public string FilePath { get; set; }

		public string IV { get; set; }
	}
}