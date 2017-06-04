using Android.App;
using Android.Content.PM;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using MvvmCross.Droid.Views;
using Microsoft.Azure.Mobile.Distribute;

namespace Countr.Droid
{
    [Activity(
        Label = "Countr"
        , MainLauncher = true
        , Icon = "@mipmap/ic_launcher"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }

        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);

            MobileCenter.Start("50855f68-c77e-4043-b353-116a0c9c45c3",
                               typeof(Analytics), 
                               typeof(Crashes)/*,
                               typeof(Distribute)*/);
        }
    }
}
