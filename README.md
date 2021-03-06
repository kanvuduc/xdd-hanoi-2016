# DEV DAYS WALKTHROUGH - QUICKSTART
This is the demo application inside session Introduction to Xamarin at XDD Hà Nội 2016.

In this demo, we will build an application
- Allow user to enter a phoneword
- Translate that phoneword to a phone number
- Allow user to call to that phone number

## ANDROID WALKTHROUGH
 
![Android UI](https://developer.xamarin.com/guides/android/getting_started/hello,android/hello,android_quickstart/Images/intro-app-examples-sml.png)

1. Create a new Android project
2. Introduce briefly about Android project struture
3. Add UI controls
    - TextView: `Enter a Phoneword`
    - EditText: hint `Enter here`
    - Button: `Translate`
    - Button: `Call`
4. Add event handlers for buttons
    - Button `Translate`
        * Support class for translating phoneword to phone number
        ```
        using System.Text;
        using System;

        namespace Core
        {
            public static class PhonewordTranslator
            {
                public static string ToNumber(string raw)
                {
                    if (string.IsNullOrWhiteSpace(raw))
                        return "";
                    else
                        raw = raw.ToUpperInvariant();

                    var newNumber = new StringBuilder();
                    foreach (var c in raw)
                    {
                        if (" -0123456789".Contains(c))
                            newNumber.Append(c);
                        else {
                            var result = TranslateToNumber(c);
                            if (result != null)
                                newNumber.Append(result);
                        }
                        // otherwise we've skipped a non-numeric char
                    }
                    return newNumber.ToString();
                }
                static bool Contains(this string keyString, char c)
                {
                    return keyString.IndexOf(c) >= 0;
                }
                static int? TranslateToNumber(char c)
                {
                    if ("ABC".Contains(c))
                        return 2;
                    else if ("DEF".Contains(c))
                        return 3;
                    else if ("GHI".Contains(c))
                        return 4;
                    else if ("JKL".Contains(c))
                        return 5;
                    else if ("MNO".Contains(c))
                        return 6;
                    else if ("PQRS".Contains(c))
                        return 7;
                    else if ("TUV".Contains(c))
                        return 8;
                    else if ("WXYZ".Contains(c))
                        return 9;
                    return null;
                }
            }
        }
        ```

        * Event handler for button `Translate`
        ```
        translatedNumber = Core.PhonewordTranslator.ToNumber(txtPhoneword.Text);

        if (!string.IsNullOrWhiteSpace(translatedNumber))
        {
            btnCall.Text = $"Call {translatedNumber}";
            btnCall.Enabled = true;
        }
        else {
            btnCall.SetText("Call");
            btnCall.Enabled = false;
        }
        ```
    - Event handler for button `Call`
        ```
        var alert = new Android.Support.V7.App.AlertDialog.Builder(this)
                        .SetMessage("Call " + translatedNumber + "?")
                        .SetNeutralButton("Call", (xsender, xe) =>
                        {
                            var callIntent = new Intent(Intent.ActionCall);
                            callIntent.SetData(Uri.Parse("tel:" + translatedNumber));
                            StartActivity(callIntent);
                        })
                        .SetNegativeButton("Cancel", delegate { })
                        .Create();

        alert.Show();
        ```

## iOS WALKTHROUGH
![iOS UI](https://developer.xamarin.com/guides/ios/getting_started/hello,_iOS/hello,iOS_quickstart/Images/image1.png)

1. Create a single view iOS project
2. Introduce briefly about project structure
3. Add UI controls
    - UILabel: `Enter a Phoneword`
    - UITextField: placeholder `Enter here`
    - UIButton: `Translate`
    - UIButton: `Call`
4. Add event handlers for buttons
- Button `Translate`
    * Support class for translating phoneword to phone number
    ```
    using System.Text;
    using System;

    namespace Core
    {
        public static class PhonewordTranslator
        {
            public static string ToNumber(string raw)
            {
                if (string.IsNullOrWhiteSpace(raw))
                    return "";
                else
                    raw = raw.ToUpperInvariant();

                var newNumber = new StringBuilder();
                foreach (var c in raw)
                {
                    if (" -0123456789".Contains(c))
                        newNumber.Append(c);
                    else {
                        var result = TranslateToNumber(c);
                        if (result != null)
                            newNumber.Append(result);
                    }
                    // otherwise we've skipped a non-numeric char
                }
                return newNumber.ToString();
            }
            static bool Contains(this string keyString, char c)
            {
                return keyString.IndexOf(c) >= 0;
            }
            static int? TranslateToNumber(char c)
            {
                if ("ABC".Contains(c))
                    return 2;
                else if ("DEF".Contains(c))
                    return 3;
                else if ("GHI".Contains(c))
                    return 4;
                else if ("JKL".Contains(c))
                    return 5;
                else if ("MNO".Contains(c))
                    return 6;
                else if ("PQRS".Contains(c))
                    return 7;
                else if ("TUV".Contains(c))
                    return 8;
                else if ("WXYZ".Contains(c))
                    return 9;
                return null;
            }
        }
    }
    ```

    * Add event handler to button `Translate`
    ```
    translatedNumber = Core.PhonewordTranslator.ToNumber(txtPhoneword.Text);

    if (!string.IsNullOrWhiteSpace(translatedNumber))
    {
        btnCall.SetTitle(string.Format($"Call {translatedNumber}"), UIControlState.Normal);
        btnCall.Enabled = true;
    }
    else {
        btnCall.SetTitle(string.Format($"Call"), UIControlState.Normal);
        btnCall.Enabled = false;
    }
    ```
- Add event handler to button `Call`
    ```
    var alert = new UIAlertController();
    alert.Message = $"Call {translatedNumber}?";

    var alertCallAction = UIAlertAction.Create(
        "Call",
        UIAlertActionStyle.Default,
        (obj) =>
        {
            var telUrl = new NSUrl($"tel:{translatedNumber}");
            UIApplication.SharedApplication.OpenUrl(telUrl);
            phoneHistory.Add(translatedNumber);
            btnViewHistory.Enabled = true;

            alert.DismissViewController(true, null);
        });
    alert.AddAction(alertCallAction);

    var alertCancelAction = UIAlertAction.Create(
        "Cancel",
        UIAlertActionStyle.Cancel,
        (obj) =>
        {
            alert.DismissViewController(true, null);
        }
    );
    alert.AddAction(alertCancelAction);

    PresentViewController(alert, true, null);
    ```

## Code sharing across platforms
As you can see, there are a lot of code duplication on these above two projects.
PCL project and SharedProject are coming to rescue.

### PCL
`PhonewordTranslator` is totally a pure C# class without any dependency on platforms.
It's the time for PCL.

1. Create a new PCL project
2. Move class `PhonewordTranslator` to PCL project
3. Add reference from Android/iOS projects to PCL project
4. Re-run

### ViewModel
There are still duplications inside ViewController and MainActivity. One of that is at the event handler for button `Translate`.
ViewModel is the solution to reduce those kinds of duplications

1. Create class `MainViewModel` in PCL project
    ```
    public class MainViewModel {

    }
    ```
2. Add private field `translatedPhonenumber`
    ```
    private string translatedNumber;
    ```
3. Add method `TryTranslate`
    ```
    public bool TryTranslate(string text, out string btnCallText)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            btnCallText = "Call";
            return false;
        }
        else {
            translatedPhonenumber = PhonewordTranslator.ToNumber(text);
            btnCallText = $"Call {translatedPhonenumber}";

            return true;
        }
    }
    ```
4. Amend class MainActivity và ViewController using new view model
- Add new field
    ```
    private MainViewModel viewModel;
    ```
- Initilize an instance of MainViewModel in method `OnCreate/ViewDidLoad`
    ```
    viewModel =  new MainViewModel();
    ```
- Amend event handler for button `Translate`
    - Android
    ```
    string btnCallText;
    btnCall.Enabled = viewModel.TryTranslate(txtPhoneword.Text, out btnCallText);
    btnCall.Text = btnCallText;
    ```
    - iOS
    ```
    string btnCallText;
    btnCall.Enabled = viewModel.TryTranslate(txtPhoneword.Text, out btnCallText);
    btnCall.SetTitle(btnCallText, UIControlState.Normal);
    ```
### Shared Project
There is still code duplication when handling button `Call`. However, there are dependencies on platform specific features.
SharedProject could help to reuse code but leverage platform specific features.

*Change the way of making a call*

1. Create interface `IPhoneCaller` in project PCL
    ```
    public interface IPhoneCaller {
        void Call(string phoneNumber);
    }
    ```
2. Create a SharedProject project
3. Create class `PhoneCaller` implementing interface `IPhoneCaller`
    ```
    using System;
    using XamarinQs.Core;

    #if __ANDROID__
    using Android.App;
    using Android.Content;
    #elif __IOS__
    using Foundation;
    using UIKit;
    #endif

    namespace Core
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

    ```
4. Add references from Android/iOS projects to SharedProject project
5. Amend code to make use of above class/interface
    - PCL
    ```
    private IPhoneCaller phoneCaller;
    public MainViewModel(IPhoneCaller phoneCaller) {
        this.phoneCaller = phoneCaller;
    }

    public void Call() {
        this.phoneCaller.Call(translatedNumber);
    }
    ```
    - Android
    ```
    viewModel = new MainViewModel(new PhoneCaller(this));

    ...

    .SetNeutralButton("Call", (xsender, xe) =>
    {
        viewModel.Call();
    })
    ````
    - iOS
    ```
    viewModel = new MainViewModel(new PhoneCaller());

    ...

    var alertCallAction = UIAlertAction.Create(
        "Call",
        UIAlertActionStyle.Default,
        (obj) =>
        {
            viewModel.Call();

            alert.DismissViewController(true, null);
        });
    ```
6. Re-run

### NuGet
We could do the same for showing a confirmation dialog. BUT, we have a communitty sharing their re-use libraries via NuGet.
We could leverage to speed up our development.

1. Add package `UserDiaglogs` to PCL, iOS, Android projects
2. Amend class `MainViewModel`
    ```
    private IPhoneCaller phoneCaller;
    private IUserDialogs userDialogs;
    public MainViewModel(IPhoneCaller phoneCaller, IUserDialogs userDialogs) {
        this.phoneCaller = phoneCaller;
        this.userDialogs = userDialogs;
    }

    public async Task<bool> Call()
    {
        var config = new ConfirmConfig
        {
            Message = $"Call {translatedPhonenumber}?",
            CancelText = "Cancel",
            OkText = "Call"
        };

        var confirmed = await this.userDialogs.ConfirmAsync(config);
        if (confirmed)
        {
            this.phoneCaller.Call(translatedPhonenumber);
            PhoneHistory.Add(translatedPhonenumber);
        }
        return confirmed;
    }
    ```
3. Amend class `MainActivity` and class `ViewController`
    - Android
    ```
    viewModel = new MainViewModel(new PhoneCaller(this), new UserDialogsImpl(() => this));

    ...

    btnCall.Click += async (sender, e) =>
    {
        await viewModel.Call();
    };
    ```
    - iOS
    ```
    viewModel = new MainViewModel(new PhoneCaller(), new UserDialogsImpl());

    ...

    btnCall.TouchUpInside += async (sender, e) =>
    {
        await viewModel.Call();
    };
    ```
4. Re-run
