using Android.Graphics;
using Android.Graphics.Drawables;
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

        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
        {
            viewModel.Counters[viewHolder.AdapterPosition].DeleteCommand.Execute();
        }

        readonly Drawable background = new ColorDrawable(Color.Red);

        public override void OnChildDraw(Canvas c, RecyclerView recyclerView,
                                         RecyclerView.ViewHolder viewHolder,
                                         float dX, float dY, int actionState,
                                         bool isCurrentlyActive)
        {
            background.SetBounds(viewHolder.ItemView.Right + (int)dX,
                                  viewHolder.ItemView.Top,
                                  viewHolder.ItemView.Right,
                                  viewHolder.ItemView.Bottom);
            background.Draw(c);

            base.OnChildDraw(c, recyclerView, viewHolder, dX, dY,
                             actionState, isCurrentlyActive);
        }
    }
}