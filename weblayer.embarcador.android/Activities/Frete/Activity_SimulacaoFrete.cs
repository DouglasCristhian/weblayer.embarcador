
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using weblayer.embarcador.android.Fragments;
using weblayer.embarcador.android.Helpers;

namespace weblayer.embarcador.android.Activities
{
    [Activity(Label = "Simulação de Frete", MainLauncher = false)]
    public class Activity_SimulacaoFrete : Activity_Base
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        EditText txtOrigem;
        EditText txtDestino;
        EditText txtValorNF;
        EditText txtPesoNF;
        EditText txtVolume;
        Button btnEnviar;

        private string codmunorigem;
        private string codmundestino;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_SimularFrete;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            FindViews();
            BindData();
            SetStyle();
        }

        private void TxtOrigem_Click(object sender, System.EventArgs e)
        {
            codmunorigem = "";

            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            Fragment_BuscarCidade dialog = new Fragment_BuscarCidade();
            dialog.DialogClosed += Dialog_DialogClosedOrigem;
            dialog.Show(transaction, "dialog");
        }

        private void Dialog_DialogClosedOrigem(object sender, Helpers.DialogEventArgs e)
        {
            var retorno = SplitString(e.ReturnValue);
            txtOrigem.Text = retorno[0];
            //codmunorigem = retorno[1];
        }

        private void TxtDestino_Click(object sender, System.EventArgs e)
        {
            codmundestino = "";

            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            Fragment_BuscarCidade dialog = new Fragment_BuscarCidade();
            dialog.DialogClosed += Dialog_DialogClosedDestino;
            dialog.Show(transaction, "dialog");
        }

        private void Dialog_DialogClosedDestino(object sender, Helpers.DialogEventArgs e)
        {
            var retorno = SplitString(e.ReturnValue);
            txtDestino.Text = retorno[0];
        }

        private void BtnEnviar_Click(object sender, System.EventArgs e)
        {
            if (!ValidateViews())
                return;

            Intent intent = new Intent();
            intent.SetClass(this, typeof(Activity_SimulacaoFreteResultado));

            intent.PutExtra("origem", codmunorigem);
            intent.PutExtra("destino", codmundestino);
            intent.PutExtra("valor", txtValorNF.Text);
            intent.PutExtra("peso", txtPesoNF.Text);
            intent.PutExtra("volume", txtVolume.Text);

            StartActivity(intent);
        }

        private void FindViews()
        {
            txtOrigem = FindViewById<EditText>(Resource.Id.txtOrigem);
            txtDestino = FindViewById<EditText>(Resource.Id.txtDestino);
            txtValorNF = FindViewById<EditText>(Resource.Id.txtValorNF);
            txtPesoNF = FindViewById<EditText>(Resource.Id.txtPesoNF);
            txtVolume = FindViewById<EditText>(Resource.Id.txtVolume);
            btnEnviar = FindViewById<Button>(Resource.Id.btnEnviar);

            GetToolbar();
        }

        private void GetToolbar()
        {
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Simular Frete";
            toolbar.InflateMenu(Resource.Menu.menu_toolbar);
            toolbar.Menu.RemoveItem(Resource.Id.action_sobre);
            toolbar.Menu.RemoveItem(Resource.Id.action_filtrar);
        }

        private void BindData()
        {
            btnEnviar.Click += BtnEnviar_Click;
            txtOrigem.Click += TxtOrigem_Click;
            txtDestino.Click += TxtDestino_Click;

            txtValorNF.AddTextChangedListener(new CurrencyConverterHelper(txtValorNF));
        }

        private void SetStyle()
        {
            txtOrigem.SetBackgroundResource(Resource.Drawable.BordaBotoes);
            txtDestino.SetBackgroundResource(Resource.Drawable.BordaBotoes);
            txtValorNF.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            txtPesoNF.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            txtVolume.SetBackgroundResource(Resource.Drawable.EditTextStyle);

        }

        private bool ValidateViews()
        {
            var validacao = true;
            if (txtOrigem.Length() == 0)
            {
                validacao = false;
                txtOrigem.Error = "Origem inválida!";
            }

            if (txtDestino.Length() == 0)
            {
                validacao = false;
                txtDestino.Error = "Destino inválido!";
            }

            if (txtValorNF.Length() == 0)
            {
                validacao = false;
                txtValorNF.Error = "Valor do NF inválido!";
            }

            if (txtPesoNF.Length() == 0)
            {
                validacao = false;
                txtPesoNF.Error = "Peso do NF inválido!";
            }

            if (txtVolume.Length() == 0)
            {
                validacao = false;
                txtVolume.Error = "Volume inválido!";
            }

            return validacao;
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

        public static string[] SplitString(string texto)
        {
            char[] delimiterChars =
            {
                '|'
            };
            return texto.Split(delimiterChars);
        }
    }
}