using Foundation;
using Countr.Core.ViewModels;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace Countr.iOS.Views
{
    public class CountersTableViewSource : MvxTableViewSource
    {
        public CountersTableViewSource(UITableView tableView)
           : base(tableView)
        {
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return (CounterTableViewCell)tableView.DequeueReusableCell("CounterCell");
        }

        public override void CommitEditingStyle(UITableView tableView,
                                        UITableViewCellEditingStyle editingStyle,
                                        NSIndexPath indexPath)
        {
            var counter = (CounterViewModel)GetItemAt(indexPath);
            counter.DeleteCommand.Execute(null);
        }
    }
}