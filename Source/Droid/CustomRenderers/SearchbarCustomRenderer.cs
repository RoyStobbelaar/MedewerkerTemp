using Android.Text;
using Android.Widget;
using Android.Graphics;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using MCCForms.Droid;

[assembly:ExportRenderer( typeof(SearchBar), typeof(SearchBarCustomRenderer) )]

namespace MCCForms.Droid
{
	public class SearchBarCustomRenderer : SearchBarRenderer
	{
		Typeface _typeFaceRegular = Typeface.CreateFromAsset (Forms.Context.Assets, "RijksoverheidSansTextTT-Regular_2_0.ttf");

		protected override void OnElementChanged( ElementChangedEventArgs<SearchBar> args )
		{
			base.OnElementChanged(args);

			SearchView searchView = (base.Control as SearchView);
			searchView.SetInputType(InputTypes.ClassText | InputTypes.TextVariationNormal);

			int editTextId = searchView.Context.Resources.GetIdentifier("android:id/search_src_text", null, null);
			EditText editTextToStyle = (searchView.FindViewById(editTextId) as EditText);
			editTextToStyle.SetTypeface (_typeFaceRegular, TypefaceStyle.Normal);
			editTextToStyle.TextSize = 14;
		}
	}
}