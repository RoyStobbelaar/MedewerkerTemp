using System;

using Android.Views;
using Android.Content;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Android.Widget;

//Used to clear emty headers in TableView for sections without a specified title - DO NOT REMOVE THIS CLASS
//Also used to set font
[assembly: ExportRenderer (typeof(TextCell), typeof(TableSectionHeaderRenderer))] 
public class TableSectionHeaderRenderer : TextCellRenderer
{
	//Typeface _typeFaceRegular = Typeface.CreateFromAsset (Android.App.Application.Context.Assets, "RijksoverheidSansTextTT-Regular_2_0.ttf");

	protected override Android.Views.View GetCellCore (Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
	{
		// Hide cells of TableSections with no title.
		var view = base.GetCellCore (item, convertView, parent, context) as ViewGroup;
		if (String.IsNullOrEmpty ((item as TextCell).Text)) {
			view.Visibility = ViewStates.Gone;
			while (view.ChildCount > 0) {
				view.RemoveViewAt (0);
			}
			view.SetMinimumHeight (0);
			view.SetPadding (0, 0, 0, 0);
		}

//		var cell = (LinearLayout) base.GetCellCore(item, convertView, parent, context);
//		var textView = (TextView)(cell.GetChildAt(1) as LinearLayout).GetChildAt(0);
//		var detailView = (TextView)(cell.GetChildAt(1) as LinearLayout).GetChildAt(1);
//
//		textView.SetTypeface (_typeFaceRegular, TypefaceStyle.Normal);
//		detailView.SetTypeface (_typeFaceRegular, TypefaceStyle.Normal);

		return view;
	}
}