using Foundation;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.Platform;
using UIKit;
using Microsoft.Azure.Mobile.Distribute;

namespace Countr.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate
    {
        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            MobileCenter.Start("cf71392c-61fb-4208-af4c-68d7e8a15c33",
                               typeof(Analytics),
                               typeof(Crashes),
                               typeof(Distribute));

#if DEBUG
            Xamarin.Calabash.Start();
#endif 

            UINavigationBar.Appearance.BarTintColor = UIColor.Orange;
            UINavigationBar.Appearance.TintColor = UIColor.DarkGray;
            UINavigationBar.Appearance.TitleTextAttributes =
               new UIStringAttributes(new NSDictionary(UIStringAttributeKey.ForegroundColor,
                                                       UIColor.DarkGray));

            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            var setup = new Setup(this, Window);
            setup.Initialize();

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            Window.MakeKeyAndVisible();

            return true;
        }
    }
}
