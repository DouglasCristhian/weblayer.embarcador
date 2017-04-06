using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using weblayer.embarcador.android.Adapters;
using weblayer.embarcador.core.BLL;
using weblayer.embarcador.core.Model;

namespace weblayer.embarcador.android.Activities
{
    [Activity(MainLauncher = false, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class Activity_Performance : Activity_Base
    {
        private string AnoSelecionado;
        private string MesSelecionado;
        ListView ListViewPerformance;
        List<Performance> ListaPerformances;
        Android.Support.V7.Widget.Toolbar toolbar;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_Performance;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            FindViews();
        }

        private void FindViews()
        {
            ListViewPerformance = FindViewById<ListView>(Resource.Id.PerformanceListView);
            Filtro_Spinner();
            GetToolbar();
            string DataFinal = (MesSelecionado + "/" + AnoSelecionado).ToString();
            this.Title = "Performance (" + DataFinal + ")";
        }

        private void GetToolbar()
        {
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_toolbar, menu);
            menu.RemoveItem(Resource.Id.action_sobre);
            menu.RemoveItem(Resource.Id.action_ajuda);
            menu.RemoveItem(Resource.Id.action_sair);
            return true;
        }

        private void Filtro_Spinner()
        {
            var prefs = Application.Context.GetSharedPreferences("MyPrefs", FileCreationMode.WorldWriteable);
            var prefEditor = prefs.Edit();

            string ano = prefs.GetString("PrefAnoPerformanceString", DateTime.Now.Year.ToString());
            AnoSelecionado = ano;

            string mes = prefs.GetString("PrefMesPerformancePosicao", DateTime.Now.Month.ToString());
            MesSelecionado = mes;

            FillList(int.Parse(AnoSelecionado), int.Parse(MesSelecionado));
        }

        private void FillList(int ano, int mes)
        {
            ListaPerformances = new PerformanceManager().GetPerformance(ano, mes);
            ListViewPerformance.Adapter = new Adapter_Performance_ListView(this, ListaPerformances);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();

                    return true;

                case Resource.Id.action_filtrar:

                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(Activity_FiltrarPerformance));
                    StartActivityForResult(intent, 0);

                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            FindViews();
        }
    }
}