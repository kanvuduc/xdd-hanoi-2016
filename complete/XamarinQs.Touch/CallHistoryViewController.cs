using System;
using System.Collections.Generic;
using UIKit;

namespace XamarinQs.Touch
{
	public partial class CallHistoryViewController : UITableViewController
	{
		public List<string> Phones { get; set; }

		protected CallHistoryViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return Phones?.Count ?? 0;
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("CELL");

			if (cell == null)
			{
				cell = new UITableViewCell(UITableViewCellStyle.Value1, "CELL");
			}

			cell.TextLabel.Text = this.Phones[indexPath.Row];

			return cell;
		}
	}
}

