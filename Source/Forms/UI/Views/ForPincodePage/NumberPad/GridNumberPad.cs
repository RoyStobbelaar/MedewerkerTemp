using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MCCForms
{
	public class GridNumberPad : Grid
	{
		public List<ButtonNumberPad> Buttons { get; set; }

		public GridNumberPad ()
		{
			Buttons = new List<ButtonNumberPad> ();

			for (int number = 0; number <= 11; number++) {
				Buttons.Add (new ButtonNumberPad (number));
			}
				
			ChangeAppearenceBasedOnDeviceType ();

			BackgroundColor = Color.FromRgb (180, 180, 180);

			Padding = 1;
			ColumnSpacing = 1;
			RowSpacing = 1;
			HorizontalOptions = LayoutOptions.FillAndExpand;

			RowDefinitions.Add (new RowDefinition { Height = new GridLength (1, GridUnitType.Star)  });
			RowDefinitions.Add (new RowDefinition { Height = new GridLength (1, GridUnitType.Star)  });
			RowDefinitions.Add (new RowDefinition { Height = new GridLength (1, GridUnitType.Star)  });
			RowDefinitions.Add (new RowDefinition { Height = new GridLength (1, GridUnitType.Star)  });

			ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star)  });
			ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star)  });
			ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star)  });

			Children.Add (Buttons[1], 0, 0);
			Children.Add (Buttons[2], 1, 0);
			Children.Add (Buttons[3], 2, 0);

			Children.Add (Buttons[4], 0, 1);
			Children.Add (Buttons[5], 1, 1);
			Children.Add (Buttons[6], 2, 1);

			Children.Add (Buttons[7], 0, 2);
			Children.Add (Buttons[8], 1, 2);
			Children.Add (Buttons[9], 2, 2);

			Children.Add (Buttons[10], 0, 3);
			Children.Add (Buttons[0], 1, 3);
			Children.Add (Buttons[11], 2, 3);
		}

		private void ChangeAppearenceBasedOnDeviceType()
		{
			if (DeviceTypeHelper.IsPhone) {
				HeightRequest = 200;
			} else if (DeviceTypeHelper.IsTablet) {
				HeightRequest = 275;
			}
		}
	}
}