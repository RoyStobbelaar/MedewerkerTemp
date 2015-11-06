using Xamarin.Forms;

namespace MCCForms
{
	public partial class MCCWizardHeaderViewCell : ViewCell
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

		string _text;
		public string Text {
			get {
				return _text;
			}
			set {
				_text = value;
				labelHeaderText.Text = _text;
			}
		}

		public MCCWizardHeaderViewCell ()
		{
			InitializeComponent();

			if (DeviceTypeHelper.IsTablet) {
				labelHeaderText.FontSize = 15;
			}
		}
	}
}