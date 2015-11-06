using System;

using Xamarin.Forms;

namespace MCCForms
{
	public partial class MCCImageViewCell : ViewCell
	{
		public static readonly BindableProperty SourceProperty = BindableProperty.Create<MCCImageViewCell, string>(item => item.Source, default(string));

		public string Source {
			get {
				return (string)GetValue(SourceProperty); 
			}
			set { 
				SetValue(SourceProperty, value); 
			}
		}

		public static readonly BindableProperty TextProperty = BindableProperty.Create<MCCImageViewCell, string>(item => item.Text, default(string));

		public string Text {
			get {
				return (string)GetValue(TextProperty); 
			}
			set { 
				SetValue(TextProperty, value); 
			}
		}

		public static readonly BindableProperty CommandProperty = BindableProperty.Create<MCCImageViewCell, Command>(item => item.Command, null);

		public Command Command {
			get { 
				return (Command)GetValue(CommandProperty); 
			}
			set { 
				SetValue(CommandProperty, value); 
			}
		}

		public MCCImageViewCell ()
		{
			InitializeComponent ();

			if (DeviceTypeHelper.IsTablet) {
				labelText.FontSize = 15;
			}
		}
	}
}