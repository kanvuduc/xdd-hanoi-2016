using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace XamarinQs.Droid
{
	[Activity(Label = "CallHistoryActivity")]
	public class CallHistoryActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.call_history);

			var phones = Intent.Extras.GetStringArrayList("PHONE_HISTORY");
			var adapter = new PhonesAdapter(phones.ToArray());

			var lstPhones = FindViewById<RecyclerView>(Resource.Id.lstPhones);
			lstPhones.SetAdapter(adapter);

			var loManager = new LinearLayoutManager(this);
			lstPhones.SetLayoutManager(loManager);

			// Create your application here
		}
	}

	class PhonesAdapter : RecyclerView.Adapter
	{
		private string[] phones;

		public PhonesAdapter(string[] phones)
		{
			this.phones = phones;
		}

		public override int ItemCount
		{
			get
			{
				return this.phones.Length;
			}
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			var phone = this.phones[position];

			(holder as PhonesViewHolder).SetData(phone);
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			var itemView = LayoutInflater.From(parent.Context)
										 .Inflate(Android.Resource.Layout.SimpleListItem1, parent, false);

			return new PhonesViewHolder(itemView);
		}
	}

	class PhonesViewHolder : RecyclerView.ViewHolder
	{
		private TextView lblPhone;
		
		public PhonesViewHolder(View itemView) : base(itemView)
		{
			lblPhone = itemView.FindViewById<TextView>(Android.Resource.Id.Text1);
		}

		public void SetData(string phone) {
			lblPhone.Text = phone;
		}
	}
}
