
using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace Zakupy.Droid.View
{
    [Activity(MainLauncher = true, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen() : base(Resource.Layout.SplashScreen)
        {

        }
    }
}