using Xamarin.Forms;

namespace MCCForms
{
	public partial class MCCDescriptionWithImageViewCell : ViewCell
	{
		string _title;
		public string Title { 
			get {
				return _title;
			}
			set {
				_title = value;
				labelTitle.Text = _title;

				if (IsRequired) {
					labelTitle.Text = labelTitle.Text += " *";
				}
			}
		}

		string _subtitle;
		public string Subtitle { 
			get {
				return _subtitle;
			}
			set {
				_subtitle = value;
				labelValue.Text = _subtitle;
			}
		}

		bool _isRequired;
		public bool IsRequired { 
			get {
				return _isRequired;
			}
			set {
				if (value != _isRequired) {
					_isRequired = value;
					labelTitle.Text = labelTitle.Text += " *";
				}
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

		public MCCDescriptionWithImageViewCell ()
		{
			InitializeComponent ();

			if (DeviceTypeHelper.IsTablet) {
				labelTitle.FontSize = 15;
				labelValue.FontSize = 15;
			}
		}
	}
}