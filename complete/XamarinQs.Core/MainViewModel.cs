using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Core;

namespace XamarinQs.Core
{
	public class MainViewModel
	{
		public List<string> PhoneHistory
		{
			get;
			set;
		}

		private string translatedPhonenumber;

		readonly IPhoneCaller phoneCaller;
		readonly IUserDialogs userDialogs;

		public MainViewModel(IPhoneCaller phoneCaller, IUserDialogs userDialogs)
		{
			this.phoneCaller = phoneCaller;
			this.userDialogs = userDialogs;

			PhoneHistory = new List<string>();
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

		public bool TryTranslate(string text, out string btnCallText)
		{
			if (string.IsNullOrWhiteSpace(text) || text.Length < 9 || text.Length > 15)
			{
				btnCallText = "Call";
				userDialogs.Toast("Please enter a valid phoneword");
				return false;
			}
			else {
				translatedPhonenumber = PhonewordTranslator.ToNumber(text);

				btnCallText = $"Call {translatedPhonenumber}";

				return true;
			}
		}
	}
}
