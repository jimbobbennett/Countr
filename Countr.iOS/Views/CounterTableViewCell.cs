using System;
using Countr.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;

namespace Countr.iOS.Views
{
   public partial class CounterTableViewCell : MvxTableViewCell
   {
      public CounterTableViewCell(IntPtr handle) : base(handle)
      {
         this.DelayBind(() =>
         {
            var set = this.CreateBindingSet<CounterTableViewCell, 
                                            CounterViewModel>();
            set.Bind(CounterName).To(vm => vm.Name);
            set.Bind(CounterCount).To(vm => vm.Count);
            set.Bind(IncrementButton).To(vm => vm.IncrementCommand);
            set.Apply();
         });
      }
   }
}