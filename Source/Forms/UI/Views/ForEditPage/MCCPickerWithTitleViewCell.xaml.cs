using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MCCForms
{
	public partial class MCCPickerWithTitleViewCell : MCCBaseTitleViewCell
	{
		public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create<MCCPickerWithTitleViewCell, string>(p => p.Placeholder, default(string));

		public string Placeholder {
			get { 
				return (string)GetValue(PlaceholderProperty); 
			}
			set { 
				SetValue(PlaceholderProperty, value); 
			}
		}

		public static readonly BindableProperty ValueProperty = BindableProperty.Create<MCCPickerWithTitleViewCell, string>(p => p.Value, default(string), BindingMode.TwoWay);

		public string Value {
			get { 
				return (string)GetValue(ValueProperty); 
			}
			set { 
				SetValue(ValueProperty, value); 
			}
		}

		public static readonly BindableProperty ItemsProperty = BindableProperty.Create<MCCPickerWithTitleViewCell, List<string>>(p => p.Items, new List<string>());

		public List<string> Items {
			get { 
				return (List<string>)GetValue(ItemsProperty); 
			}
			set { 
				SetValue(ItemsProperty, value); 
			}
		}

		public MCCPickerWithTitleViewCell ()
		{
			InitializeComponent ();
		
			if (DeviceTypeHelper.IsTablet) {
				labelTitle.FontSize = 15;
			}

			//Ugly bug fix to get it work on Android
			buttonFocus.Clicked += (object sender, EventArgs e) => 
			{
				Device.BeginInvokeOnMainThread (() => {
					picker.Focus ();
				});
			};
		}

//		protected override void OnTapped () //does not work on Android
//		{
//			base.OnTapped ();
//
//			picker.Focus ();
//		}
	}
}