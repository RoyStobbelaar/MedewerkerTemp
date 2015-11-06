using Xamarin.Forms;

/// <summary>
/// Color themes.
/// App color themes - Belastingdienst, Douane and Toeslagen
/// </summary>

namespace MCCForms
{
	public static class ColorThemes
	{
		public static ColorTheme ThemeBelastingdienst
		{
			get {
				return new ColorTheme {
					ColorPincodePageTitleH1 = Color.FromRgb (21, 66, 115), 
					ColorPincodePageTitleH2 =  Color.FromRgb (0, 0, 0), 

					ColorPincodePageTextInput = Color.FromRgb (0, 0, 0), 
					ColorPincodePageInputVisited = Color.FromRgb (143, 202, 231),
					ColorPincodePageInputActive = Color.FromRgb (184, 221, 240),
					ColorPincodePageInputInactive = Color.FromRgb (225, 225, 225),

					ColorSlider = Color.FromRgb (143, 202, 231),
					ColorSwitch = Color.FromRgb (143, 202, 231),
					ColorToolbarText = Color.Black,
					ColorToolbarButton = Color.Black,
					ColorSearchbar = Color.FromRgb (221, 239, 248),
					ColorButton = Color.FromRgb (19, 54, 82),

					ColorTabbarBackground = Color.White,
					ColorTabbarSelectedItem = Color.FromRgb (17, 124, 198),
					ColorTabbarUnselectedItem = Color.FromRgb(155, 155, 155),

					ColorToolbarBackground = Color.FromRgb (143, 202, 231),
					ColorPageBackground = Color.FromRgb (221, 239, 248),
					ColorSecondToolbarBackground = Color.FromRgb (143, 202, 231)
				};
			}
		}

		public static ColorTheme ThemeToeslagen
		{
			get {
				return new ColorTheme {
					ColorPincodePageTitleH1 = Color.FromRgb (66, 20, 95), 
					ColorPincodePageTitleH2 =  Color.FromRgb (0, 0, 0), 

					ColorPincodePageTextInput = Color.FromRgb (255, 255, 255),
					ColorPincodePageInputVisited = Color.FromRgb (213, 43, 30),
					ColorPincodePageInputActive = Color.FromRgb (237, 166, 161),
					ColorPincodePageInputInactive = Color.FromRgb (225, 225, 225),

					ColorSlider = Color.FromRgb (213, 43, 30),
					ColorSwitch = Color.FromRgb (213, 43, 30),
					ColorToolbarText = Color.White,
					ColorToolbarButton = Color.White,
					ColorSearchbar = Color.FromRgb (249, 223, 221),
					ColorButton = Color.FromRgb (19, 54, 82),

					ColorTabbarBackground = Color.White,
					ColorTabbarSelectedItem = Color.FromRgb (17, 124, 198),
					ColorTabbarUnselectedItem = Color.FromRgb(155, 155, 155),

					ColorToolbarBackground = Color.FromRgb (213, 43, 30),
					ColorPageBackground = Color.FromRgb (249, 223, 221),
					ColorSecondToolbarBackground = Color.FromRgb (213, 43, 30)
				};
			}
		}

		public static ColorTheme ThemeDouane
		{
			get {
				return new ColorTheme {
					ColorPincodePageTitleH1 = Color.FromRgb (39, 89, 55), 
					ColorPincodePageTitleH2 =  Color.FromRgb (0, 0, 0), 

					ColorPincodePageTextInput = Color.FromRgb (0, 0, 0), 
					ColorPincodePageInputVisited = Color.FromRgb (118, 210, 182),
					ColorPincodePageInputActive = Color.FromRgb (167, 225, 206),
					ColorPincodePageInputInactive = Color.FromRgb (225, 225, 225),

					ColorSlider = Color.FromRgb (118, 210, 182),
					ColorSwitch = Color.FromRgb (118, 210, 182),
					ColorToolbarText = Color.Black,
					ColorToolbarButton = Color.Black,
					ColorSearchbar = Color.FromRgb (214, 241, 233),
					ColorButton = Color.FromRgb (39, 89, 55),

					ColorTabbarBackground = Color.White,
					ColorTabbarSelectedItem = Color.FromRgb (17, 124, 198),
					ColorTabbarUnselectedItem = Color.FromRgb(155, 155, 155),

					ColorToolbarBackground = Color.FromRgb (118, 210, 182),
					ColorPageBackground = Color.FromRgb (214, 241, 233),
					ColorSecondToolbarBackground = Color.FromRgb (118, 210, 182)
				};
			}
		}
	}

	public class ColorTheme
	{
		public Color ColorPincodePageTitleH1 { get; set; }
		public Color ColorPincodePageTitleH2 { get; set; }

		public Color ColorPincodePageTextInput { get; set; }
		public Color ColorPincodePageInputVisited { get; set; }
		public Color ColorPincodePageInputActive { get; set; }
		public Color ColorPincodePageInputInactive { get; set; }

		public Color ColorSlider { get; set; }
		public Color ColorSwitch { get; set; }
		public Color ColorToolbarText { get; set; }
		public Color ColorToolbarButton { get; set; }
		public Color ColorSearchbar { get; set; }
		public Color ColorButton { get; set; }

		public Color ColorTabbarBackground { get; set; }
		public Color ColorTabbarSelectedItem { get; set; }
		public Color ColorTabbarUnselectedItem { get; set; }
		
		public Color ColorToolbarBackground { get; set; }
		public Color ColorPageBackground { get; set; }
		public Color ColorSecondToolbarBackground { get; set; }
	}
}