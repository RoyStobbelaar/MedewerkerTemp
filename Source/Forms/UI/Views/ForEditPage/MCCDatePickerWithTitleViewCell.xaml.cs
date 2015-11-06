using System;

using Xamarin.Forms;

namespace MCCForms
{
	public partial class MCCDatePickerWithTitleViewCell : MCCBaseTitleViewCell
	{
		public static readonly BindableProperty DateProperty = BindableProperty.Create<MCCDatePickerWithTitleViewCell, DateTime>(
				p => p.Date, default(DateTime), defaultBindingMode: BindingMode.TwoWay);

		public DateTime Date {
			get { 
				return (DateTime)GetValue(DateProperty); 
			}
			set { 
				SetValue(DateProperty, value); 
			}
		}

		public MCCDatePickerWithTitleViewCell ()
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
				datePicker.Focus ();
			});
		}
	}
}