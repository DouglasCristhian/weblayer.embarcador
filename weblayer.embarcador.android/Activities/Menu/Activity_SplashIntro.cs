
using Android.App;
using Android.OS;
using Android.Views;
using weblayer.embarcador.android.Activities;

namespace weblayer.embarcador.android
{
    [Activity(NoHistory = true, MainLauncher = true)]
    public class Activity_SplashIntro : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SplashLayout);
            System.Threading.ThreadPool.QueueUserWorkItem(o => LoadActivity());
        }

        private void LoadActivity()
        {
            System.Threading.Thread.Sleep(5000); //Simulate a long pause    
            RunOnUiThread(() => StartActivity(typeof(Activity_Login)));
        }
    }
}