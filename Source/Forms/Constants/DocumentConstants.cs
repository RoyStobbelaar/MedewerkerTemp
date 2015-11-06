
/// <summary>
/// Document constants.
/// Returns platform specific root path to write to and read from
/// </summary>

using Xamarin.Forms;

namespace MCCForms
{
	public static class DocumentConstants
	{
		public static string DocumentsPath
		{
			get {
				return DependencyService.Get<IDocumentsPath> ().GetDocumentsPath();
			}
		}
	}
}