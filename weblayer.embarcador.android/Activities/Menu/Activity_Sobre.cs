using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace weblayer.embarcador.android.Activities
{
    [Activity(Label = "Sobre")]
    public class Activity_Sobre : Activity_Base
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        private ListView mListView;
        private List<string> mItems;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_Sobre;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Menu.RemoveItem(Resource.Id.action_filtrar);

            FindViews();
            BindData();
        }

        private void FindViews()
        {
            mListView = FindViewById<ListView>(Resource.Id.listaSobre);
        }

        private void BindData()
        {
            mItems = new List<string>();

            mItems.Add("Novidades");
            mItems.Add("Versão\n" + GetVersion());

            mListView.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mItems);

            mListView.ItemClick += MListView_ItemClick;
        }

        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e.Id == 0)
            {
                StartActivity(typeof(Activity_Novidades));
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private string GetVersion()
        {
            return Application.Context.PackageManager.GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionName;
        }
    }
}