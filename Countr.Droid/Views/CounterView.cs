using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Countr.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Countr.Droid.Views
{
    [Activity(Label = "@string/AddNewCounterName")]
    public class CounterView : MvxAppCompatActivity<CounterViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.counter_view);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.OnCreateOptionsMenu(menu);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.InflateMenu(Resource.Menu.new_counter_menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    ViewModel.CancelCommand.Execute(null);
                    return true;
                case Resource.Id.action_save_counter:
                    ViewModel.SaveCommand.Execute(null);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}
