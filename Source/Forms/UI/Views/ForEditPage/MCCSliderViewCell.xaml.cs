using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MCCForms
{
	public partial class MCCSliderViewCell : MCCBaseTitleViewCell
	{
		public static readonly BindableProperty ValueProperty = 
			BindableProperty.Create<MCCSliderViewCell, double>(p => p.Value, default(double),defaultBindingMode: BindingMode.TwoWay);

		public double Value {
			get { 
				return (double)GetValue(ValueProperty); 
			}
			set { 
				SetValue(ValueProperty, value); 
			}
		}

		public MCCSliderViewCell ()
		{
			InitializeComponent ();

			if (DeviceTypeHelper.IsTablet) {
				labelTitle.FontSize = 15;
			}
		}
	}
}