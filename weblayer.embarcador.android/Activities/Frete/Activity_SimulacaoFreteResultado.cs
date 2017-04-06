using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using weblayer.embarcador.android.Adapters;
using weblayer.embarcador.core.BLL;
using weblayer.embarcador.core.Model;

namespace weblayer.embarcador.android.Activities
{
    [Activity(Label = "Resultado da Simulação", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class Activity_SimulacaoFreteResultado : Activity_Base
    {
        ListView ListViewResult;
        List<SimulacaoFrete> ListaSimulacao;
        TextView EmpytText;
        Android.Support.V7.Widget.Toolbar toolbar;

        private string codmunorigem;
        private string codmundestino;
        private string valornf;
        private string valorpesonf;
        private string volumenf;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_SimulacaoFreteResultado;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            FindViews();
            BindData();
        }

        private void FindViews()
        {
            ListViewResult = FindViewById<ListView>(Resource.Id.lstResultadoSimulacaoFrete);
            EmpytText = FindViewById<TextView>(Resource.Id.txtListEmpty);

            GetToolbar();
        }

        private void GetToolbar()
        {
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Resultado da Simulação";
            toolbar.InflateMenu(Resource.Menu.menu_toolbar);
            toolbar.Menu.RemoveItem(Resource.Id.action_sobre);
            toolbar.Menu.RemoveItem(Resource.Id.action_filtrar);
        }

        private void BindData()
        {
            codmunorigem = Intent.GetStringExtra("origem");
            codmundestino = Intent.GetStringExtra("destino");
            valornf = Intent.GetStringExtra("valor");
            valorpesonf = Intent.GetStringExtra("peso");
            volumenf = Intent.GetStringExtra("volume");

            var simulafrete = new SimulacaoFreteManager();

            ListaSimulacao = simulafrete.GetSimulacaoFrete(codmunorigem, codmundestino, decimal.Parse(valornf), decimal.Parse(valorpesonf), decimal.Parse(volumenf));

            ListViewResult.Adapter = new Adapter_SimulacaoFrete_ListView(this, ListaSimulacao);
            ListViewResult.ItemClick += OnListItemClick;
            EmpytText.Text = simulafrete.mensagem;
            ListViewResult.EmptyView = EmpytText;
        }

        private void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var ListViewSimulacaoFrete = sender as ListView;
            var l = ListaSimulacao[e.Position];

            Intent intent = new Intent();
            intent.SetClass(this, typeof(Activity_TabelaFreteResultado));
            intent.PutExtra("dadossimulacao", Newtonsoft.Json.JsonConvert.SerializeObject(l));

            StartActivity(intent);

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
    }
}