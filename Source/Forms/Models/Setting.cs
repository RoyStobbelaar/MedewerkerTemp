
/// <summary>
/// Setting.
/// Used to write key-value pairs to the local database
/// </summary>

using SQLite;

namespace MCCForms
{
	public class Setting
	{
		[PrimaryKey]
		public string Key { get; set; }

		public string Value { get; set; }
	}
}