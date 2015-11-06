using Android.App;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;

using Xamarin.Forms;

using MCCForms.Droid;

[assembly: Dependency (typeof(Dialog_Droid))] //How to use in Forms project: DependencyService.Get<IDialog> ().ShowProgressDialog ("The message you want to show");

namespace MCCForms.Droid
{
	public class Dialog_Droid : IDialog
	{
		static Dialog _dialog;
		Typeface _typeFaceRegular = Typeface.CreateFromAsset (Android.App.Application.Context.Assets, "RijksoverheidSansTextTT-Regular_2_0.ttf");

		public void ShowProgressDialog (string message)
		{
			if (_dialog != null) {
				HideProgressDialog ();
			}

			_dialog = new Dialog(Forms.Context);
			_dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
			_dialog.SetCancelable(false);
			_dialog.SetCanceledOnTouchOutside(false);

			Window window = _dialog.Window;
			window.SetLayout(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.MatchParent);
			window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));

			_dialog.SetContentView (Resource.Layout.dialog);

			var textView = (TextView)_dialog.FindViewById(Resource.Id._textView);
			textView.SetTypeface (_typeFaceRegular, TypefaceStyle.Normal);
			textView.Text = message;

			_dialog.Show();
		}
		public void HideProgressDialog ()
		{
			if (_dialog != null) {
				_dialog.Hide ();
				_dialog = null;
			}
		}
	}
}