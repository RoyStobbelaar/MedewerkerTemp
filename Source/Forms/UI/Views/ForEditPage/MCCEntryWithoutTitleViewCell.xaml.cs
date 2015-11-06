using Xamarin.Forms;

namespace MCCForms
{
	public partial class MCCEntryWithoutTitleViewCell : ViewCell
	{
		public static readonly BindableProperty ValueProperty = BindableProperty.Create<MCCEntryWithoutTitleViewCell, string>(p => p.Value, default(string),defaultBindingMode: BindingMode.TwoWay);

		public string Value {
			get { 
				return (string)GetValue(ValueProperty); 
			}
			set { 
				SetValue(ValueProperty, value); 
			}
		}

		string _placeholder;
		public string Placeholder {
			get {
				return _placeholder;
			}
			set {
				_placeholder = value;
				entryValue.Placeholder = _placeholder;
			}
		}

		public MCCEntryWithoutTitleViewCell ()
		{
			InitializeComponent();
		}
	}
}