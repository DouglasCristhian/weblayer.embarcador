
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
            base.OnCreate(bundle);

            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.SplashLayout);

            if (Resources.Configuration.Orientation == Android.Content.Res.Orientation.Portrait)
            {
                this.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
            }
            else
            {
                this.RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;
            }

            System.Threading.ThreadPool.QueueUserWorkItem(o => LoadActivity());
        }

        private void LoadActivity()
        {
            System.Threading.Thread.Sleep(2000); //2 segundos
            RunOnUiThread(() => StartActivity(typeof(Activity_Login)));
        }
    }
}