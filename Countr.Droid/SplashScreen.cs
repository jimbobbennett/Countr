using Android.App;
using Android.Content.PM;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using MvvmCross.Droid.Views;

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

            AppCenter.Start("3de451f4-50bc-4db7-9a96-5032966ecd6a",
                            typeof(Analytics),
                            typeof(Crashes),
                            typeof(Distribute));
        }
    }
}
