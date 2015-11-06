using Xamarin.Forms;

namespace MCCForms
{
	public partial class MCCSwitchViewCell : MCCBaseTitleViewCell
	{
		public static readonly BindableProperty SubtitleProperty = BindableProperty.Create<MCCSwitchViewCell, string>(p => p.Subtitle, default(string));

		public string Subtitle {
			get { 
				return (string)GetValue(SubtitleProperty); 
			}
			set { 
				SetValue(SubtitleProperty, value); 
			}
		}

		public static readonly BindableProperty IsToggledProperty = BindableProperty.Create<MCCSwitchViewCell, bool>(p => p.IsToggled, default(bool), defaultBindingMode: BindingMode.TwoWay);

		public bool IsToggled {
			get { 
				return (bool)GetValue(IsToggledProperty); 
			}
			set { 
				SetValue(IsToggledProperty, value); 
			}
		}

		public MCCSwitchViewCell()
		{
			InitializeComponent();

			if (DeviceTypeHelper.IsTablet) {
				labelTitle.FontSize = 15;
				labelSubtitle.FontSize = 15;
			}
		}
	}
}