using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Support.V7.App;
using Acr.UserDialogs;
using XamarinQs.Core;
using XamarinQs.Droid;

namespace XamarinQs.Droid
{
	[Activity(Label = "Droid", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/AppTheme")]
	public class MainActivity : AppCompatActivity
	{
		private Button btnTranslate;
		private Button btnCall;
		private EditText txtPhoneword;
		private Button btnViewHistory;

		private MainViewModel viewModel;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			viewModel = new MainViewModel(new PhoneCaller(this), new UserDialogsImpl(() => this));

			// Get our controls from the layout resource,
			// and attach an event to it
			txtPhoneword = FindViewById<EditText>(Resource.Id.txtPhoneword);
			btnTranslate = FindViewById<Button>(Resource.Id.btnTranslate);
			btnCall = FindViewById<Button>(Resource.Id.btnCall);
			btnCall.Enabled = false;
			btnViewHistory = FindViewById<Button>(Resource.Id.btnViewHistory);
			btnViewHistory.Enabled = false;


			btnTranslate.Click += (sender, e) =>
			{
				string btnCallText;
				btnCall.Enabled = viewModel.TryTranslate(txtPhoneword.Text, out btnCallText);
				btnCall.Text = btnCallText;

			};

			btnCall.Click += async (sender, e) =>
			{
				await viewModel.Call();

				btnViewHistory.Enabled = viewModel.PhoneHistory.Count > 0;
			};

			btnViewHistory.Click += (sender, e) =>
			{
				var intent = new Intent(this, typeof(CallHistoryActivity));
				intent.PutStringArrayListExtra("PHONE_HISTORY", viewModel.PhoneHistory);

				StartActivity(intent);
			};
		}
	}
}

