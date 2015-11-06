using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MCCForms
{
	public partial class MCCTimePickerWithTitleViewCell : MCCBaseTitleViewCell
	{
		public static readonly BindableProperty DateProperty = BindableProperty.Create<MCCTimePickerWithTitleViewCell, DateTime>(
			p => p.Time, default(DateTime), defaultBindingMode: BindingMode.TwoWay);

		public DateTime Time {
			get { 
				return (DateTime)GetValue(DateProperty); 
			}
			set { 
				SetValue(DateProperty, value); 
			}
		}

		public MCCTimePickerWithTitleViewCell ()
		{
			InitializeComponent ();

			if (DeviceTypeHelper.IsTablet) {
				labelTitle.FontSize = 15;
				labelDate.FontSize = 15;
			}
		}

		protected override void OnTapped ()
		{
			base.OnTapped ();

			Device.BeginInvokeOnMainThread (() => {
				timePicker.Focus ();
			});
		}
	}
}