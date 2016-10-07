// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace XamarinQs.Touch
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton btnCall { get; set; }

		[Outlet]
		UIKit.UIButton btnTranslate { get; set; }

		[Outlet]
		UIKit.UIButton btnViewHistory { get; set; }

		[Outlet]
		UIKit.UITextField txtPhoneword { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (txtPhoneword != null) {
				txtPhoneword.Dispose ();
				txtPhoneword = null;
			}

			if (btnTranslate != null) {
				btnTranslate.Dispose ();
				btnTranslate = null;
			}

			if (btnCall != null) {
				btnCall.Dispose ();
				btnCall = null;
			}

			if (btnViewHistory != null) {
				btnViewHistory.Dispose ();
				btnViewHistory = null;
			}
		}
	}
}
