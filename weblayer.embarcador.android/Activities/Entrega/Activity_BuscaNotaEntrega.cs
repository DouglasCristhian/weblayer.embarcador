using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Threading;
using weblayer.embarcador.android.Adapters;
using weblayer.embarcador.core.BLL;
using weblayer.embarcador.core.Model;
using ZXing.Mobile;

namespace weblayer.embarcador.android.Activities
{
    [Activity(MainLauncher = false, Label = "Buscar Nota Fiscal", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class Activity_BuscaNotaEntrega : Activity_Base
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        MobileBarcodeScanner scanner;
        ListView ListViewNota;
        List<NotaFiscal> ListaNotas;
        Button btnEscanear;
        EditText txtNumNota;
        TextView EmpytText;
        bool PROSSEGUIR;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_BuscaNotaEntrega;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ListViewNota = FindViewById<ListView>(Resource.Id.NotaListView);

            FindViews();
            SetStyle();
            BindData();

            MobileBarcodeScanner.Initialize(Application);
            scanner = new MobileBarcodeScanner();
        }

        private void FindViews()
        {
            btnEscanear = FindViewById<Button>(Resource.Id.btnEscanear);
            txtNumNota = FindViewById<EditText>(Resource.Id.txtNumNota);
            EmpytText = FindViewById<TextView>(Resource.Id.txtListEmpty);

            GetToolbar();
        }

        private void GetToolbar()
        {
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.InflateMenu(Resource.Menu.menu_toolbar);
            toolbar.Menu.RemoveItem(Resource.Id.action_sobre);
            toolbar.Menu.RemoveItem(Resource.Id.action_filtrar);
        }

        private void BindData()
        {
            btnEscanear.Click += BtnEscanear_Click;
            ListViewNota.ItemClick += OnListItemClick;

            txtNumNota.TextChanged += TxtNumNota_TextChanged;
        }

        private void TxtNumNota_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            FillList();
        }

        private void SetStyle()
        {
            txtNumNota.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            //btnEscanear.SetBackgroundResource(Resource.Drawable.BordaBotoes);
        }

        private void FillList()
        {
            ListaNotas = new NotaFiscalManager().GetNotaFiscal(txtNumNota.Text);
            ListViewNota.Adapter = new Adapter_NotaFiscal_ListView(this, ListaNotas);
            ListViewNota.EmptyView = EmpytText;
        }

        private void BtnEscanear_Click(object sender, EventArgs e)
        {
            PermissoesGarantidas();
            if (PROSSEGUIR == true)
            {
                Scanner();
            }
        }

        public void PermissoesGarantidas()
        {
            if ((ActivityCompat.CheckSelfPermission(this, Manifest.Permission.Camera) == Permission.Granted))
            {
                PROSSEGUIR = true;
                Scanner();
            }
            else
            {
                string[] permissionRequest = { Manifest.Permission.Camera };
                RequestPermissions(permissionRequest, 0);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if ((requestCode == 0))
            {
                if (grantResults[0] == Permission.Granted)
                {
                    Scanner();
                }
                else
                    Toast.MakeText(this, "Não é possível usar a câmera sem as devidas permissões", ToastLength.Long).Show();
                return;
            }
        }

        private async void Scanner()
        {
            ZXing.Result result = null;
            scanner.TopText = "Aguarde o escaneamento do código de barras";

            new Thread(new ThreadStart(delegate
            {
                while (result == null)
                {
                    scanner.AutoFocus();
                    Thread.Sleep(2000);
                }
            })).Start();

            result = await scanner.Scan();
            HandleScanResult(result);
        }

        public void HandleScanResult(ZXing.Result result)
        {
            if (result == null)
                return;

            txtNumNota.Text = result.ToString();
            FillList();
        }

        private void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var ListViewNotaClick = sender as ListView;
            var t = ListaNotas[e.Position];

            Intent intent = new Intent();
            intent.SetClass(this, typeof(Activity_InformaEntrega));

            //Passa a string do objeto para a próxima tela
            intent.PutExtra("JsonNota", Newtonsoft.Json.JsonConvert.SerializeObject(t));

            StartActivityForResult(intent, 0);

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                var mensagem = data.GetStringExtra("mensagem");
                Toast.MakeText(this, mensagem, ToastLength.Long).Show();

                FillList();
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
    }
}