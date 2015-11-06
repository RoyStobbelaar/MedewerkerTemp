using Xamarin.Forms;

namespace MCCForms
{
	public partial class MCCStandardViewCell : ViewCell
	{
		public static readonly BindableProperty TextProperty = BindableProperty.Create<MCCStandardViewCell, string>(item => item.Text, default(string));

		public string Text {
			get { 
				return (string)GetValue(TextProperty); 
			}
			set { 
				SetValue(TextProperty, value); 
			}
		}

		public static readonly BindableProperty CommandProperty = BindableProperty.Create<MCCStandardViewCell, Command>(item => item.Command, null);

		public Command Command {
			get { 
				return (Command)GetValue(CommandProperty); 
			}
			set { 
				SetValue(CommandProperty, value); 
			}
		}

		public MCCStandardViewCell ()
		{
			InitializeComponent ();

			if (DeviceTypeHelper.IsTablet) {
				labelText.FontSize = 16;
			}
		}
	}
}