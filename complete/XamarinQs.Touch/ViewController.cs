using System;
using Acr.UserDialogs;
using Foundation;
using UIKit;
using XamarinQs.Core;
using XamarinQs.Droid;

namespace XamarinQs.Touch
{
	public partial class ViewController : UIViewController
	{
		MainViewModel viewModel;


		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			viewModel = new MainViewModel(new PhoneCaller(), new UserDialogsImpl());

			btnTranslate.TouchUpInside += (sender, e) =>
			{
				string btnCallText;
				btnCall.Enabled = viewModel.TryTranslate(txtPhoneword.Text, out btnCallText);
				btnCall.SetTitle(btnCallText, UIControlState.Normal);
			};

			btnCall.TouchUpInside += async (sender, e) =>
			{
				await viewModel.Call();

				btnViewHistory.Enabled = viewModel.PhoneHistory.Count > 0;
			};
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			var dest = segue.DestinationViewController as CallHistoryViewController;
			dest.Phones = viewModel.PhoneHistory;
		}
	}
}
