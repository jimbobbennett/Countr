using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Countr.Core.ViewModels;

namespace Countr.Droid.Views
{
    public class SwipeItemTouchHelperCallback : ItemTouchHelper.SimpleCallback
    {
        readonly CountersViewModel viewModel;

        public SwipeItemTouchHelperCallback(CountersViewModel viewModel)
           : base(0, ItemTouchHelper.Start)
        {
            this.viewModel = viewModel;
        }

        public override bool OnMove(RecyclerView recyclerView,
                            RecyclerView.ViewHolder viewHolder,
                            RecyclerView.ViewHolder target)
        {
            return true;
        }

        public override void OnSwiped(RecyclerView.ViewHolder viewHolder,
                                      int direction)
        {
            viewModel.Counters[viewHolder.AdapterPosition].DeleteCommand.Execute();
        }
    }
}