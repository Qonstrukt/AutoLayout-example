using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace Autolayout.iOS
{
	public partial class MasterViewController : UITableViewController, IUISplitViewControllerDelegate
	{
		private bool collapseSecondViewController;
		private readonly List<object> objects = new List<object> ();

		public MasterViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("Master", "Master");
		}

		public IList<object> Objects {
			get { return objects; }
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			NavigationItem.LeftBarButtonItem = EditButtonItem;
			NavigationItem.RightBarButtonItem = new UIBarButtonItem (UIBarButtonSystemItem.Add, AddNewItem);

			if (SplitViewController != null) {
				collapseSecondViewController = true;
				SplitViewController.Delegate = this;
			}
		}

		// Customize the number of sections in the table view.
		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return objects.Count;
		}

		public override nfloat EstimatedHeight (UITableView tableView, NSIndexPath indexPath)
		{
			return tableView.RowHeight;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("Cell", indexPath);
			var titleLabel = cell.ViewWithTag (1) as UILabel;
			var textLabel = cell.ViewWithTag (2) as UILabel;

			if (titleLabel == null) {
				cell.TextLabel.Text = objects [indexPath.Row].ToString ();
			} else {
				titleLabel.Text = objects [indexPath.Row].ToString ();
				textLabel.Text = objects [indexPath.Row].ToString ();
			}

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			collapseSecondViewController = false;
		}

		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			// Return false if you do not want the specified item to be editable.
			return true;
		}

		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if (editingStyle == UITableViewCellEditingStyle.Delete) {
				// Delete the row from the data source.
				objects.RemoveAt (indexPath.Row);
				tableView.DeleteRows (new [] { indexPath }, UITableViewRowAnimation.Fade);
			}
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			var detailViewController = segue.DestinationViewController as DetailViewController;
			if (detailViewController != null) {
				var indexPath = TableView.IndexPathForSelectedRow;
				var item = Objects [indexPath.Row];

				detailViewController.SetDetailItem (item);
			}

			var navigationController = segue.DestinationViewController as UINavigationController;
			if (navigationController != null) {
				detailViewController = navigationController.TopViewController as DetailViewController;
				if (detailViewController != null) {
					var indexPath = TableView.IndexPathForSelectedRow;
					var item = Objects [indexPath.Row];

					detailViewController.SetDetailItem (item);
				}
			}
		}

		private void AddNewItem (object sender, EventArgs args)
		{
			var text = DateTime.Now.ToString ();
			for (int i = 0; i < Objects.Count; i++)
				text += " " + text;

			Objects.Insert (0, text);

			using (var indexPath = NSIndexPath.FromRowSection (0, 0))
				TableView.InsertRows (new [] { indexPath }, UITableViewRowAnimation.Automatic);
		}

		[Export ("splitViewController:collapseSecondaryViewController:ontoPrimaryViewController:")]
		public bool CollapseSecondViewController (UISplitViewController splitViewController, UIKit.UIViewController secondaryViewController, UIViewController primaryViewController)
		{
			return collapseSecondViewController;
		}
	}
}

