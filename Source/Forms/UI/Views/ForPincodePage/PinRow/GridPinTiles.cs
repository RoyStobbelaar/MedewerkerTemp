using System.Collections.Generic;

using Xamarin.Forms;

namespace MCCForms
{
	public class GridPinTiles : Grid
	{
		public List<PinTile> PinTiles { get; set; }

		public GridPinTiles ()
		{
			PinTiles = new List<PinTile> ();

			for (int i = 0; i < 5; i++) {
				PinTiles.Add (new PinTile ());
			}

			ChangeAppearenceBasedOnDeviceType ();

			Children.Add (PinTiles[0], 0, 0);
			Children.Add (PinTiles[1], 1, 0);
			Children.Add (PinTiles[2], 2, 0);
			Children.Add (PinTiles[3], 3, 0);
			Children.Add (PinTiles[4], 4, 0);

			HorizontalOptions = LayoutOptions.FillAndExpand;
			VerticalOptions = LayoutOptions.Fill;
		}

		void ChangeAppearenceBasedOnDeviceType()
		{
			if (DeviceTypeHelper.IsPhone) {
				ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star)  });
				ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star)  });
				ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star)  });
				ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star)  });
				ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star)  });

				RowDefinitions.Add (new RowDefinition { Height =  65});
			} 
			else if (DeviceTypeHelper.IsTablet) {
				ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star)  });
				ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star)  });
				ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star)  });
				ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star)  });
				ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star)  });

				RowDefinitions.Add (new RowDefinition { Height = 75  });
			}
		}

		public void SetColorTheme(ColorTheme appTheme)
		{
			foreach (var item in PinTiles) {
				item.SetColorTheme (appTheme);
			}
		}
	}
}