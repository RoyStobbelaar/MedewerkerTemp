using Xamarin.Forms;

namespace MCCForms
{
	public abstract class MCCBaseTitleViewCell : ViewCell
	{
		public static readonly BindableProperty TitleProperty = BindableProperty.Create<MCCBaseTitleViewCell, string>(p => p.Title, default(string));

		public string Title {
			get {
				return (string)GetValue(TitleProperty);
			}
			set {
				SetValue(TitleProperty, value);
			}
		}
	}
}