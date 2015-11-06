using System;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using MCCForms;
using MCCForms.iOS;

[assembly: ExportRenderer (typeof (TextCell), typeof (TextCellRegularFontCustomRenderer))]

namespace MCCForms.iOS
{
	public class TextCellRegularFontCustomRenderer : TextCellRenderer
	{
		public override UITableViewCell GetCell (Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var cellView = base.GetCell (item, reusableCell, tv);

			cellView.BackgroundColor = Color.Transparent.ToUIColor();
			cellView.TextLabel.Font = UIFont.FromName("RijksoverheidSansTextTT-Regular", cellView.TextLabel.Font.PointSize);
			cellView.DetailTextLabel.Font = UIFont.FromName("RijksoverheidSansTextTT-Regular", cellView.DetailTextLabel.Font.PointSize);
		
			return cellView;
		}
	}
}