using System;

using Xamarin.Forms;

namespace MCCForms
{
	public class NavigationInfoButtonLayout : StackLayout
	{
		public Button Button;
		public Image Image;

		public NavigationInfoButtonLayout ()
		{
			Image = new Image {
				Source = "vraagteken.png",
				Aspect = Aspect.AspectFill,
				Scale = 0.5
			};

			Button = new Button {
				BackgroundColor = Color.Transparent
			};

			var relativeLayout = new RelativeLayout {
				HeightRequest = 50,
				WidthRequest = 50
			};

			relativeLayout.Children.Add (Image,
				Constraint.Constant (0),
				Constraint.Constant (0),
				Constraint.RelativeToParent ((parent) => parent.Width),
				Constraint.RelativeToParent ((parent) => parent.Height));

			relativeLayout.Children.Add (Button,
				Constraint.Constant (0),
				Constraint.Constant (0),
				Constraint.RelativeToParent ((parent) => parent.Width),
				Constraint.RelativeToParent ((parent) => parent.Height));
			
			Children.Add (relativeLayout);
		}
	}
}