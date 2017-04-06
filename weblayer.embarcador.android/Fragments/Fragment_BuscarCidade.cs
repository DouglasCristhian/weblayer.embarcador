
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using weblayer.embarcador.android.Adapters;
using weblayer.embarcador.android.Helpers;
using weblayer.embarcador.core.BLL;
using weblayer.embarcador.core.Model;

namespace weblayer.embarcador.android.Fragments
{
    public class Fragment_BuscarCidade : DialogFragment
    {
        public event EventHandler<DialogEventArgs> DialogClosed;
        ListView ListViewCidades;
        List<Cidade> ListaCidades;
        string Retorno;
        EditText txtInformarCidade;
        Button btnPesquisarCidade;
        private ArrayAdapter _adapter;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Android.OS.Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Fragment_BuscarCidade, container, false);

            btnPesquisarCidade = (Button)view.FindViewById(Resource.Id.btnPesquisarCidade);
            txtInformarCidade = (EditText)view.FindViewById(Resource.Id.txtInformarCidade);
            ListViewCidades = (ListView)view.FindViewById(Resource.Id.CidadeListView);

            _adapter = new ArrayAdapter(this.Activity, Resource.Layout.Fragment_BuscarCidade, ListaCidades);
            txtInformarCidade.TextChanged += TxtInformarCidade_TextChanged;

            BindData();
            SetStyle();
            // FillList();

            Retorno = "";

            btnPesquisarCidade.Click += BtnPesquisarCidade_Click;

            return view;
        }

        private void TxtInformarCidade_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            //_adapter.Filter.InvokeFilter(e.ToString);

        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }

        private void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var ListViewCidades = sender as ListView;
            var t = ListaCidades[e.Position];
            var cidadeestado = t.ds_cidade + "  " + t.ds_estado + "|" + t.ds_codmun;

            Retorno = cidadeestado;

            Dismiss();
        }

        private void BtnPesquisarCidade_Click(object sender, EventArgs e)
        {
            FillList();
        }

        public override void OnDismiss(IDialogInterface dialog)
        {
            base.OnDismiss(dialog);
            if (DialogClosed != null)
            {
                DialogClosed(this, new DialogEventArgs { ReturnValue = Retorno });
            }

        }

        private void SetStyle()
        {
            txtInformarCidade.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            btnPesquisarCidade.SetBackgroundResource(Resource.Drawable.BordaBotoes);
        }

        private void BindData()
        {
            ListViewCidades.ItemClick += OnListItemClick;
        }

        private void FillList(/*string /*filtro*/)
        {
            ListaCidades = new CidadeManager().GetCidade(/*filtro*/);
            ListViewCidades.Adapter = new Adapter_Cidade_ListView(this.Activity, ListaCidades);

        }

    }
}