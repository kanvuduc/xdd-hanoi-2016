# DEV DAYS WALKTHROUGH - QUICKSTART
Đây là nội dung bài demo trong bài Giới thiệu về Xamarin tại buổi XDD Hà Nội - 2016.

Trong bài, ta sẽ xây dựng một ứng dụng cho phép
- Cho phép nhập một chuỗi các kí tự
- Dịch chuỗi nhập vào ra số điện thoại
- Cho phép thực hiện cuộc gọi tới số điện thoại vừa dịch được

## ANDROID WALKTHROUGH
 
![Giao diện Android](https://developer.xamarin.com/guides/android/getting_started/hello,android/hello,android_quickstart/Images/intro-app-examples-sml.png)

1. Tạo một dự án Android mới
2. Giới thiệu qua cấu trúc của dự án Android
3. Thực hiện thêm các UI controls
    - TextView: Hiện tiêu đề `Enter a Phoneword`
    - EditText: Để nhập chuỗi kĩ tự, gợi ý `Enter here`
    - Button: Để dịch chuỗi kĩ tự ra số điện thoại, tiêu đề `Translate`
    - Button: Để thực hiện cuộc gọi tới số điện thoại vừa dịch được, tiêu đề `Call`
4. Thêm các logic xử lý khi người dụng nhấn vào các nút
    - Nút `Translate`
        * Class hỗ trợ việc dịch chuỗi kí tự ra chuỗi số
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

        * Logic cho nút `Translate`
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
    - Nút `Call`
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
![Giao diện iOS](https://developer.xamarin.com/guides/ios/getting_started/hello,_iOS/hello,iOS_quickstart/Images/image1.png)

1. Tạo ứng dụng iOS mới
2. Giới thiệu qua cấu trúc dự án iOS
3. Thực hiện thêm các UI controls
    - UILabel: Hiện tiêu đề `Enter a Phoneword`
    - UITextField: Để nhập chuỗi kĩ tự, gợi ý `Enter here`
    - UIButton: Để dịch chuỗi kĩ tự ra số điện thoại, tiêu đề `Translate`
    - UIButton: Để thực hiện cuộc gọi tới số điện thoại vừa dịch được, tiêu đề `Call`
4. Thêm các logic xử lý khi người dụng nhấn vào các nút
- Nút `Translate`
    * Class hỗ trợ việc dịch chuỗi kí tự ra chuỗi số
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

    * Logic cho nút `Translate`
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
- Nút `Call`
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

## Chia sẻ code giữa các nền tảng
Hiện tại chúng ta thấy, code đang bị lặp khá nhiều, nhất là class PhonewordTranslator.
SharedProject/PCL sinh ra để giúp giải quyết vấn đề này.

### PCL
Ta thấy class `PhonewordTranslator` hoàn toàn là một class thuần C#, không phụ thuộc vào Android/iOS.
PCL sẽ giúp ta chia sẻ những đoạn code chung
1. Tạo một dự án PCL
2. Sao chép class `PhonewordTranslator` sang dự án vừa tạo
4. Thêm tham chiếu từ dự án Android/iOS tới dự án vừa tạo
5. Chạy lại

### ViewModel
Ta thấy trong 2 calss ViewController và MainActivity vẫn còn rất nhiều đoạn code lặp, điển hình là khi thực hiện dịch từ chuỗi ra số điện thoại.
ViewModel sẽ giúp chúng ta chia sẻ những logic như vậy

1. Tạo class `MainViewModel` trong dự án PCL
    ```
    public class MainViewModel {

    }
    ```
2. Thêm thuộc tính `translatedPhonenumber`
    ```
    private string translatedNumber;
    ```
3. Thêm method `TryTranslate`
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
4. Cập nhật class MainActivity và ViewController để sử dụng code mới
- Khai báo một thuộc tính mới
    ```
    private MainViewModel viewModel;
    ```
- Khởi tạo MainViewModel trong `OnCreate/ViewDidLoad`
    ```
    viewModel =  new MainViewModel();
    ```
- Cập nhập event handler cho nút `Translate`
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
Vẫn còn code lặp: Phần code cho việc xử lý event handler của nút `Call` là khá giống nhau về mặt khái niệm/logic, nhưng lại không thể chuyển sang PCL được vì code là khác nhau hoàn toàn giữa Android và iOS. Giải pháp là định nghĩa interface trong PCL, triển khai trong mỗi nền tảng kết hợp với SharedProject.

Bắt đầu với việc thực hiện cuộc gọi

1. Tạo interface `IPhoneCaller` trong dự án PCL
    ```
    public interface IPhoneCaller {
        void Call(string phoneNumber);
    }
    ```
2. Tạo một dự án SharedProject
3. Tạo class `PhoneCaller` thừa kế interface `IPhoneCaller`
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
4. Thêm tham chiếu từ dự án Android/iOS tới dự án vừa tạo
5. Thay phần code thực hiện cuộc gọi 
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
6. Chạy lại

### NuGet
Vẫn còn code lặp: Cả hai bên Android/iOS đều hiển thị một cửa sổ xác nhận xem người dùng nên thực hiện cuộc gọi tới số vừa được dịch ra không. Về mặt khái niệm và logic là giống nhau, chỉ có code là khác nhau, ta hoàn toàn có thể làm giống như phần code vừa làm ở trên.

NHƯNG, ta không phải làm vậy, ta có thể sử dụng lại code từ cộng đồng đã chia sẻ qua NuGet.

1. Thêm package `UserDiaglogs` vào các dự án PCL, iOS, Android
2. Cập nhập trong `MainViewModel`
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
3. Cập nhật trong `MainActivity` và `ViewController`
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
4. Chạy lại
