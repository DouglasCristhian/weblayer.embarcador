using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using System;
using weblayer.embarcador.core.Model;

namespace weblayer.embarcador.android.Activities
{
    [Activity(Label = "Resultado da Simulação", MainLauncher = false)]
    public class Activity_TabelaFreteResultado : Activity_Base
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        private SimulacaoFrete simu;
        TextView txtTransp;
        TextView txtFret;
        TextView txtFreteImpos;
        WebView webview1;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_TabelaResultadoFrete;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var dadossimulados = Intent.GetStringExtra("dadossimulacao");
            simu = Newtonsoft.Json.JsonConvert.DeserializeObject<SimulacaoFrete>(dadossimulados);

            FindViews();
            BindData();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_toolbar, menu);
            menu.RemoveItem(Resource.Id.action_sobre);
            menu.RemoveItem(Resource.Id.action_sair);
            menu.RemoveItem(Resource.Id.action_contato);
            return true;
        }

        private void FindViews()
        {
            txtTransp = FindViewById<TextView>(Resource.Id.txt1);
            txtFret = FindViewById<TextView>(Resource.Id.txt2);
            txtFreteImpos = FindViewById<TextView>(Resource.Id.txt3);

            webview1 = FindViewById<WebView>(Resource.Id.webView1);
            webview1.Settings.JavaScriptEnabled = true;

            GetToolbar();
        }

        private void GetToolbar()
        {
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "";
            toolbar.InflateMenu(Resource.Menu.menu_toolbar);
            toolbar.Menu.RemoveItem(Resource.Id.action_filtrar);
        }

        private void BindData()
        {
            txtTransp.Text = "Transportadora: " + simu.ds_transportadora;
            txtFret.Text = "Frete S/ Imposto: " + simu.vl_frete;
            txtFreteImpos.Text = "Frete C/ Imposto: " + simu.vl_frete_imposto;

            String content =
            "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
            "<html><head>" +
            "<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />" +
            "<head><body>";

            content += simu.ds_memoriacalculo + "</body></html>";

            webview1.LoadData(content, "text/html; charset=utf-8", "UTF-8");


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

        public class MeuWebViewClient : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                view.LoadUrl(url);
                return true;
            }
        }

    }
}