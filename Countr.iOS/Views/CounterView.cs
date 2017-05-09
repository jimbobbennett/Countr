using System;
using Countr.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace Countr.iOS.Views
{
   [MvxFromStoryboard]
   public partial class CounterView : MvxViewController
   {
      public CounterView(IntPtr handle) : base(handle)
      {
      }

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         var button = new UIBarButtonItem(UIBarButtonSystemItem.Done);
         NavigationItem.SetRightBarButtonItem(button, false);

         var set = this.CreateBindingSet<CounterView, CounterViewModel>();
         set.Bind(CounterName).To(vm => vm.Name);
         set.Bind(button).To(vm => vm.SaveCommand);
         set.Apply();
      }
   }
}

