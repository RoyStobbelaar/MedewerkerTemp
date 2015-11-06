using Xamarin.Forms;

namespace MCCForms
{
	public partial class MCCWizardFooterViewCell : ViewCell
	{
		Color _backgroundColor;
		public Color BackgroundColor { 
			get { 
				return _backgroundColor;
			}
			set { 
				View.BackgroundColor = value;
				_backgroundColor = value;
			}
		}

		public static readonly BindableProperty TextProperty = BindableProperty.Create<MCCWizardFooterViewCell, string>(item => item.Text, default(string));

		public string Text {
			get { 
				return (string)GetValue(TextProperty); 
			}
			set { 
				SetValue(TextProperty, value); 
			}
		}

		public static readonly BindableProperty CommandProperty = BindableProperty.Create<MCCWizardFooterViewCell, Command>(item => item.Command, null);

		public Command Command {
			get { 
				return (Command)GetValue(CommandProperty); 
			}
			set { 
				SetValue(CommandProperty, value); 
			}
		}

		public MCCWizardFooterViewCell ()
		{
			InitializeComponent ();

			if (DeviceTypeHelper.IsTablet) {
				button.FontSize = 15;
			}
		}
	}
}