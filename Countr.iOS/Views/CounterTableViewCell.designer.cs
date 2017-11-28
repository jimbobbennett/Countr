// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Countr.iOS.Views
{
    [Register ("CounterTableViewCell")]
    partial class CounterTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CounterCount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CounterName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton IncrementButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CounterCount != null) {
                CounterCount.Dispose ();
                CounterCount = null;
            }

            if (CounterName != null) {
                CounterName.Dispose ();
                CounterName = null;
            }

            if (IncrementButton != null) {
                IncrementButton.Dispose ();
                IncrementButton = null;
            }
        }
    }
}