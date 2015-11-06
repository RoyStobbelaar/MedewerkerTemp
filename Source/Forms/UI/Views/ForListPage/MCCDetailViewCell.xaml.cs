using Xamarin.Forms;

namespace MCCForms
{
	public partial class MCCDetailViewCell : ViewCell
	{
		public static readonly BindableProperty TextProperty = BindableProperty.Create<MCCDetailViewCell, string>(item => item.Text, default(string));

		public string Text {
			get { 
				return (string)GetValue(TextProperty); 
			}
			set { 
				SetValue(TextProperty, value); 
			}
		}

		public static readonly BindableProperty DetailProperty = BindableProperty.Create<MCCDetailViewCell, string>(item => item.Detail, default(string));

		public string Detail {
			get { 
				return (string)GetValue(DetailProperty); 
			}
			set { 
				SetValue(DetailProperty, value); 
			}
		}

		public static readonly BindableProperty CommandProperty = BindableProperty.Create<MCCDetailViewCell, Command>(item => item.Command, null);

		public Command Command {
			get { 
				return (Command)GetValue(CommandProperty); 
			}
			set { 
				SetValue(CommandProperty, value); 
			}
		}

		public MCCDetailViewCell ()
		{
			InitializeComponent ();

			if (DeviceTypeHelper.IsTablet) {
				labelText.FontSize = 15;
				labelDetail.FontSize = 15;
			}
		}
	}
}