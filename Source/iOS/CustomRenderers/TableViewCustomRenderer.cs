using System;

using UIKit;
using CoreGraphics;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using MCCForms.iOS;

[assembly: ExportRenderer (typeof (TableView), typeof (TableViewCustomRenderer))]

namespace MCCForms.iOS
{
	public class TableViewCustomRenderer : TableViewRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<TableView> e)
		{
			base.OnElementChanged (e);

			if (Control == null) {
				return;
			} else {
				//Removes separator lines for empty listview cells
				Control.TableFooterView = new UIView (new CGRect ());
			}
		}
	}
}