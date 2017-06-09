using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Globalization;
using System.Threading;
using weblayer.embarcador.android.Helpers;
using weblayer.embarcador.core.Model;

namespace weblayer.embarcador.android.Activities
{
    [Activity(Label = "Informar Entrega")]
    public class Activity_InformaEntrega : Activity_Base
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        private TextView txtNomeCliente;
        private TextView txtValor;
        private TextView txtNota;
        private TextView txtData;
        public TextView txtHora;
        private TextView lblMensagem;
        private Button btnConfirmarEntrega;
        private NotaFiscal nota;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_InformaEntrega;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //string da nota
            var jsonnota = Intent.GetStringExtra("JsonNota");

            //transforma a string no objeto
            nota = Newtonsoft.Json.JsonConvert.DeserializeObject<NotaFiscal>(jsonnota);

            FindViews();
            SetStyles();
            BindData();
        }

        private void FindViews()
        {
            GetToolbar();

            txtNomeCliente = FindViewById<TextView>(Resource.Id.txtNomeCliente);
            txtValor = FindViewById<TextView>(Resource.Id.txtValorNota);
            txtNota = FindViewById<TextView>(Resource.Id.txtNota);
            txtData = FindViewById<TextView>(Resource.Id.txtDataEntrega);
            txtHora = FindViewById<TextView>(Resource.Id.txtHoraEntrega);
            lblMensagem = FindViewById<TextView>(Resource.Id.lblMensagem);
            btnConfirmarEntrega = FindViewById<Button>(Resource.Id.btnConfirmarEntrega);
        }

        private void GetToolbar()
        {
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Informar Entrega";
            toolbar.InflateMenu(Resource.Menu.menu_toolbar);
            toolbar.Menu.RemoveItem(Resource.Id.action_sobre);
            toolbar.Menu.RemoveItem(Resource.Id.action_filtrar);
            toolbar.Menu.RemoveItem(Resource.Id.action_contato);
        }

        private void BindData()
        {
            txtNomeCliente.Text = nota.ds_cliente;
            txtValor.Text = nota.ds_valor;
            txtNota.Text = nota.ds_numeronota + "/" + nota.ds_serienota;
            lblMensagem.Text = "";
            txtData.Text = "";
            txtHora.Text = "";
            btnConfirmarEntrega.Visibility = ViewStates.Visible;
            btnConfirmarEntrega.Click += BtnConfirmarEntrega_Click;

            if (nota.dt_entrega.HasValue)
            {
                txtData.Text = nota.dt_entrega.Value.ToString("dd/MM/yyyy");
                txtHora.Text = nota.dt_entrega.Value.ToString("HH:mm"); //Caso a nota já tenha sido entegue, mostrar a data de entrega.
                btnConfirmarEntrega.Visibility = ViewStates.Invisible;
                txtData.Enabled = false;
                txtHora.Enabled = false;
            }
            else
            {
                txtData.Text = DateTime.Now.ToString("dd/MM/yyyy"); //Caso contrário sugerir a data de hoje.
                txtHora.Text = DateTime.Now.ToString("HH:mm");
                txtData.Click += EventtxtData_Click;
                txtHora.Click += EventtxtHora_Click;
            }

            txtNomeCliente.Enabled = false;
            txtValor.Enabled = false;
            txtNota.Enabled = false;

        }

        private void SetStyles()
        {
            txtData.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            txtValor.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            txtNota.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            txtNomeCliente.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            txtHora.SetBackgroundResource(Resource.Drawable.EditTextStyle);
        }

        private void EnviaDadosEntrega()
        {
            try
            {
                //var dataentrega = DateTime.Parse(txtData.Text);

                string data = (txtData.Text + " " + txtHora.Text);
                var dataentrega = DateTime.Parse(data, CultureInfo.CreateSpecificCulture("pt-BR"));

                var notamanager = new core.BLL.NotaFiscalManager();

                var retorno = notamanager.InformarEntregaNotaFiscal(nota.id_nota, dataentrega);
                if (retorno)
                {
                    //volto para a tela que chamou passando sucesso!
                    Intent myIntent = new Intent(this, typeof(Activity_InformaEntrega));
                    myIntent.PutExtra("mensagem", notamanager.mensagem);
                    SetResult(Result.Ok, myIntent);
                    Finish();
                }
                else
                {
                    //Permancecer nessa tela e exibir a mensagem do erro
                    lblMensagem.Text = notamanager.mensagem;
                }


            }
            catch (Exception ex)
            {
                //exibir a mensagem do erro
                lblMensagem.Text = ex.Message;
            }

        }

        private void EventtxtData_Click(object sender, EventArgs e)
        {
            //Call Fragment
            DatePickerHelper frag = DatePickerHelper.NewInstance(delegate (DateTime time)
            {
                txtData.Text = time.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"));
            });

            frag.Show(FragmentManager, DatePickerHelper.TAG);
        }

        private void EventtxtHora_Click(object sender, EventArgs e)
        {
            //Call Fragment
            TimePickerHelper frag = TimePickerHelper.NewInstance(delegate (DateTime time)
            {
                txtHora.Text = time.ToString("HH:mm");
            });

            frag.Show(FragmentManager, TimePickerHelper.TAG);
        }

        private void BtnConfirmarEntrega_Click(object sender, EventArgs e)
        {

            var progressDialog = ProgressDialog.Show(this, "Por favor aguarde...", "Enviando os dados...", true);
            new Thread(new ThreadStart(delegate
            {
                Thread.Sleep(1000);

                //LOAD METHOD TO GET ACCOUNT INFO
                RunOnUiThread(() => EnviaDadosEntrega());

                //HIDE PROGRESS DIALOG
                RunOnUiThread(() => progressDialog.Hide());
            })).Start();

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