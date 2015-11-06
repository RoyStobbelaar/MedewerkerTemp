using Android.App;
using Android.Graphics.Drawables;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using MCCForms.Droid;
using Android.Text;
using Android.Text.Style;
using Android.Graphics;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(NavigationCustomRenderer))]

namespace MCCForms.Droid
{
	public class NavigationCustomRenderer : NavigationRenderer
	{
		//Typeface _typeFaceRegular = Typeface.CreateFromAsset (Android.App.Application.Context.Assets, "RijksoverheidSansTextTT-Regular_2_0.ttf");

		protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
		{
			base.OnElementChanged (e);

			RemoveAppIconFromActionBar ();
		}

		void RemoveAppIconFromActionBar()
		{
			var actionBar = ((Activity)Context).ActionBar;
			actionBar.SetIcon (new ColorDrawable(Android.Graphics.Color.Transparent));
//
//			SpannableString st = new SpannableString("Test of font");
//
//			TypefaceSpan robotoTFS = new TypefaceSpan("RijksoverheidSansTextTT-Regular_2_0.ttf");
//
//			st.SetSpan(robotoTFS, 0, st.Length(), SpanTypes.ExclusiveExclusive);
//
//
//			actionBar.TitleFormatted = st;

//			SpannableString st1 = new SpannableString("Test");
//			TypefaceSpan robotoTFS = new TypefaceSpan("RijksoverheidSansTextTT-Regular_2_0.ttf");
//			st1.SetSpan(robotoTFS, 0, st1.Length(), SpanTypes.ExclusiveExclusive);
//
//			Java.Lang.ICharSequence sequence1;
//			sequence1 = st1.SubSequenceFormatted(0, st1.Length());
//
//			actionBar.TitleFormatted = sequence1;

//			SpannableString st1 = new SpannableString("Test");
//			var x = new TypefaceSpan (actionBar, "RijksoverheidSansTextTT-Regular_2_0.ttf");
//			st1.SetSpan(x, 0, st1.Length(), SpanTypes.ExclusiveExclusive);
//
//			Java.Lang.ICharSequence sequence1;
//			sequence1 = st1.SubSequenceFormatted(0, st1.Length());
//
//			actionBar.TitleFormatted = sequence1;
		}
	}
}