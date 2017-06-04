﻿using System;
using Countr.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace Countr.iOS.Views
{
    [MvxFromStoryboard]
    public partial class CountersView : MvxTableViewController
    {
        public CountersView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var source = new CountersTableViewSource(TableView);
            TableView.Source = source;

            var button = new UIBarButtonItem(UIBarButtonSystemItem.Add);
            NavigationItem.SetRightBarButtonItem(button, false);
            button.AccessibilityIdentifier = "add_counter_button";

            var set = this.CreateBindingSet<CountersView, CountersViewModel>();
            set.Bind(source).To(vm => vm.Counters);
            set.Bind(button).To(vm => vm.ShowAddNewCounterCommand);
            set.Apply();
        }
    }
}

