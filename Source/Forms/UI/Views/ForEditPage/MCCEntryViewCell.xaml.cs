using Xamarin.Forms;

namespace MCCForms
{
	public partial class MCCEntryViewCell : MCCBaseTitleViewCell
	{
		public static readonly BindableProperty ValueProperty = BindableProperty.Create<MCCEntryViewCell, string>(p => p.Value, default(string), defaultBindingMode: BindingMode.TwoWay);

		public string Value {
			get { 
				return (string)GetValue(ValueProperty); 
			}
			set { 
				SetValue(ValueProperty, value); 
			}
		}

		public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create<MCCEntryViewCell, string>(p => p.Placeholder, default(string));

		public string Placeholder {
			get { 
				return (string)GetValue(PlaceholderProperty); 
			}
			set { 
				SetValue(PlaceholderProperty, value); 
			}
		}

		public MCCEntryViewCell()
		{
			InitializeComponent();

			Tapped += ((object sender, System.EventArgs e) => {
				_entryValue.Focus();
			});

			if (DeviceTypeHelper.IsTablet) {
				labelTitle.FontSize = 15;
			}
		}
	}
}