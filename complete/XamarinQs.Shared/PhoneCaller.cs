using System;
using XamarinQs.Core;

#if __ANDROID__
using Android.App;
using Android.Content;
#elif __IOS__
using Foundation;
using UIKit;
#endif

namespace XamarinQs.Droid
{
	public class PhoneCaller : IPhoneCaller
	{
#if __ANDROID__
		private Activity activity;
		public PhoneCaller(Activity activity)
		{
			this.activity = activity;
		}
#endif

		public void Call(string phoneNumber)
		{
			var url = $"tel:{phoneNumber}";

			#if __IOS__
			var telUrl = new NSUrl(url);
			UIApplication.SharedApplication.OpenUrl(telUrl);
			#elif __ANDROID__
			var callIntent = new Intent(Intent.ActionCall);
			callIntent.SetData(Android.Net.Uri.Parse(url));
			this.activity.StartActivity(callIntent);
			#endif

		}
	}
}
