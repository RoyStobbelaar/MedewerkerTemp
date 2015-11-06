using Xamarin.Forms;

namespace MCCForms
{
	public class PinTile : Grid
	{
		LabelRegularFont _labelPin;
		Color _colorActive;
		Color _colorInactive;
		Color _colorSet;
		Color _colorText;

		public PinTile ()
		{
			SetColorTheme(AppConfigConstants.AppTheme);

			_labelPin = new LabelRegularFont {
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				TextColor = _colorText,
				XAlign = TextAlignment.Center,
				YAlign = TextAlignment.Center
			};

			ChangeAppearenceBasedOnDeviceType ();

			VerticalOptions = LayoutOptions.Fill;
			HorizontalOptions = LayoutOptions.FillAndExpand;

			Children.Add (_labelPin);
		}

		void ChangeAppearenceBasedOnDeviceType (){
			if (DeviceTypeHelper.IsPhone) {
				_labelPin.FontSize = 65;
			} 
			else if (DeviceTypeHelper.IsTablet) {
				_labelPin.FontSize = 90;
			}
		}

		public void ShowStar(){
			BackgroundColor = _colorSet;
			_labelPin.Text = "*";
		}

		public void SetActive(){
			BackgroundColor = _colorActive;
			_labelPin.Text = "";
		}

		public void SetInActive(){
			BackgroundColor = _colorInactive;
			_labelPin.Text = "";
		}

		public void SetColorTheme(ColorTheme appTheme)
		{
			_colorActive = appTheme.ColorPincodePageInputActive;
			_colorInactive = appTheme.ColorPincodePageInputInactive;
			_colorSet = appTheme.ColorPincodePageInputVisited;
			_colorText = appTheme.ColorPincodePageTextInput;
		}

		protected override void OnSizeAllocated (double width, double height)
		{
			base.OnSizeAllocated (width, height);

			if (width != -1) {

				Device.BeginInvokeOnMainThread (() => {

					//Adjust height of pintile so it is equal to the width
					var tileLayout = new Rectangle (Bounds.X, Bounds.Y, Bounds.Width, width);
					this.Layout (tileLayout);

					this.ParentView.Layout (new Rectangle (ParentView.Bounds.X, ParentView.Bounds.Y, ParentView.Bounds.Width, width));

					//Ugly bug fix for iOS to get text centered vertically in label - needed in Xamarin.Forms 1.4.0.6341
					int yPositionLabel = 0;

					if (DeviceTypeHelper.IsIos) {
						if(Bounds.Height <= 65){
							yPositionLabel = 11;
						}
						else{
							yPositionLabel = 14;
						}
					}
					else if (DeviceTypeHelper.IsAndroid) {
						if(Bounds.Height > 55 && Bounds.Height < 70){
							yPositionLabel = 5;
						}
					}

					_labelPin.Layout (new Rectangle (_labelPin.X, yPositionLabel, Bounds.Width, Bounds.Height));
				});
			}
		}
	}
}