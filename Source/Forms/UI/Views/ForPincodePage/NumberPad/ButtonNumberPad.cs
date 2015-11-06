using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MCCForms
{
	public class ButtonNumberPad : RelativeLayout
	{
		public static event EventHandler ButtonNumberPadClicked;

		LabelRegularFont _labelToShow;
		Image _imageToShow;
		Color _colorBackgroundButtonOnClick;
		Color _colorBackgroundButton;
		Color _colorTextOnClick;
		Color _colorText;
		int _number; 

		public ButtonNumberPad (int number)
		{
			_colorBackgroundButtonOnClick = Color.FromRgb (243, 243, 243);
			_colorBackgroundButton = Color.White;
			_colorTextOnClick = Color.FromRgb (153, 153, 153);
			_colorText = Color.Black;
		
			BackgroundColor = _colorBackgroundButton;

			_number = number;

			_labelToShow = new LabelRegularFont {
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				XAlign = TextAlignment.Center,
				YAlign = TextAlignment.Center,
				TextColor = _colorText
			};

			if (number == 11) { //Show backspace image
				_imageToShow = new Image {
					Source = "Backspace.png",
					Scale = 0.5,
					HeightRequest = 40,
					WidthRequest = 40
				};

				Children.Add (_imageToShow,
					Constraint.Constant (0),
					Constraint.Constant (0),
					Constraint.RelativeToParent ((parent) => parent.Width),
					Constraint.RelativeToParent ((parent) => parent.Height));
			}
			else { //Show number label
				if (number != 10) { //10 = empty label
					_labelToShow.Text = number.ToString ();
				}
				Children.Add (_labelToShow,
					Constraint.Constant (0),
					Constraint.Constant (0),
					Constraint.RelativeToParent ((parent) => parent.Width),
					Constraint.RelativeToParent ((parent) => parent.Height));
			}

			Button button = new Button { BackgroundColor = Color.Transparent, ClassId = number.ToString () };
			button.Clicked += OnButtonClicked;
				
			Children.Add (button,
				Constraint.Constant (0),
				Constraint.Constant (0),
				Constraint.RelativeToParent ((parent) => parent.Width),
				Constraint.RelativeToParent ((parent) => parent.Height));

			VerticalOptions = LayoutOptions.FillAndExpand;
			HorizontalOptions = LayoutOptions.FillAndExpand;

			ChangeAppearenceBasedOnDeviceType ();
		}

		void ChangeAppearenceBasedOnDeviceType()
		{
			if (DeviceTypeHelper.IsPhone) {
				_labelToShow.FontSize = 30;
			} else if (DeviceTypeHelper.IsTablet) {
				_labelToShow.FontSize = 40;
			}
		}

		async void OnButtonClicked(Object s, EventArgs e)
		{
			//Change color when button is clicked
			this.BackgroundColor = _colorBackgroundButtonOnClick;
			_labelToShow.TextColor = _colorTextOnClick;

			await Task.Delay (100);
			this.BackgroundColor = _colorBackgroundButton;
			_labelToShow.TextColor = _colorText;

			EventHandler handler = ButtonNumberPadClicked;
			if (handler != null) {
				handler (this, new ButtonNumberPadClickedEventArgs { 
					ClickedNumber = _number
				});
			}
		}
	}

	public class ButtonNumberPadClickedEventArgs : EventArgs
	{
		public int ClickedNumber { get; set; }
	}
}