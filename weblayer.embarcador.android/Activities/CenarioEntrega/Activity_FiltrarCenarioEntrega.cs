    using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using weblayer.embarcador.android.Adapters;

namespace weblayer.embarcador.android.Activities
{
    [Activity(Label = "Filtro")]
    public class Activity_FiltrarCenarioEntrega : Activity_Base
    {
        private Spinner spinnerMesCenarioEntrega;
        private Spinner spinnerAnoCenarioEntrega;
        int MesEntregaPosicao;
        int AnoEntregaPosicao;
        private Button btnLimparFiltro;
        private List<mSpinner> spinnerAnoLista;
        private List<mSpinner> spinnerMesLista;
        public string MyPREFERENCES = "MyPrefs";
        List<mSpinner> minhalista = new List<mSpinner>();

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_Filtrar;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            FindViews();
            BindData();
            SetStyle();

            spinnerAnoLista = PopulateSpinnerAno();
            spinnerMesLista = PopulateSpinnerMes();

            spinnerAnoCenarioEntrega.Adapter = new ArrayAdapter<mSpinner>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, spinnerAnoLista);
            spinnerMesCenarioEntrega.Adapter = new ArrayAdapter<mSpinner>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, spinnerMesLista);

            var prefs = Application.Context.GetSharedPreferences(MyPREFERENCES, FileCreationMode.WorldReadable);
            MesEntregaPosicao = int.Parse(prefs.GetString("PrefMesCenarioEntrega", DateTime.Now.Month.ToString()));
            AnoEntregaPosicao = prefs.GetInt("PrefAnoCenarioEntrega", 0);

            spinnerAnoCenarioEntrega.SetSelection(getIndexByValue(spinnerAnoCenarioEntrega, AnoEntregaPosicao));
            spinnerMesCenarioEntrega.SetSelection(getIndexByValue(spinnerMesCenarioEntrega, MesEntregaPosicao - 1));

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_filtrar:
                    SaveForm();
                    return true;

                case Android.Resource.Id.Home:
                    Finish();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_toolbar_filtro, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        private int getIndexByValue(Spinner spinner, long myId)
        {
            int index = 0;

            var adapter = (ArrayAdapter<mSpinner>)spinner.Adapter;
            for (int i = 0; i < spinner.Count; i++)
            {
                if (adapter.GetItemId(i) == myId)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private void FindViews()
        {
            spinnerMesCenarioEntrega = FindViewById<Spinner>(Resource.Id.spinnerMes);
            spinnerAnoCenarioEntrega = FindViewById<Spinner>(Resource.Id.spinnerAno);
            btnLimparFiltro = FindViewById<Button>(Resource.Id.btnLimparFiltro);
        }

        private void BindData()
        {
            btnLimparFiltro.Click += BtnLimparFiltro_Click;
        }

        private void BtnLimparFiltro_Click(object sender, System.EventArgs e)
        {
            spinnerMesCenarioEntrega.SetSelection(DateTime.Now.Month - 1);
            spinnerAnoCenarioEntrega.SetSelection(0);
        }

        private List<mSpinner> PopulateSpinnerMes()
        {
            minhalista.Add(new mSpinner(1, "Janeiro"));
            minhalista.Add(new mSpinner(2, "Fevereiro"));
            minhalista.Add(new mSpinner(3, "Março"));
            minhalista.Add(new mSpinner(4, "Abril"));
            minhalista.Add(new mSpinner(5, "Maio"));
            minhalista.Add(new mSpinner(6, "Junho"));
            minhalista.Add(new mSpinner(7, "Julho"));
            minhalista.Add(new mSpinner(8, "Agosto"));
            minhalista.Add(new mSpinner(9, "Setembro"));
            minhalista.Add(new mSpinner(10, "Outubro"));
            minhalista.Add(new mSpinner(11, "Novembro"));
            minhalista.Add(new mSpinner(12, "Dezembro"));

            return minhalista;
        }

        private List<mSpinner> PopulateSpinnerAno()
        {
            List<mSpinner> minhalista = new List<mSpinner>();

            int ano = DateTime.Now.Year;

            minhalista.Add(new mSpinner(0, ano.ToString()));
            minhalista.Add(new mSpinner(1, (ano - 1).ToString()));
            minhalista.Add(new mSpinner(2, (ano - 2).ToString()));

            return minhalista;
        }

        private void SetStyle()
        {
            spinnerMesCenarioEntrega.SetBackgroundResource(Resource.Drawable.EditTextStyle);
            spinnerAnoCenarioEntrega.SetBackgroundResource(Resource.Drawable.EditTextStyle);
        }

        private void SaveForm()
        {
            var prefs = Application.Context.GetSharedPreferences("MyPrefs", FileCreationMode.WorldWriteable);
            var prefEditor = prefs.Edit();

            string posicaoMes = spinnerMesCenarioEntrega.SelectedItemPosition.ToString();

            prefEditor.PutString("PrefMesCenarioEntrega", (int.Parse(posicaoMes) + 1).ToString());
            prefEditor.PutInt("PrefAnoCenarioEntrega", spinnerAnoCenarioEntrega.SelectedItemPosition);
            prefEditor.PutString("PrefAnoCenarioEntregaString", spinnerAnoCenarioEntrega.SelectedItem.ToString());
            prefEditor.Commit();

            Toast.MakeText(this, "Preferências de filtro atualizadas", ToastLength.Short).Show();

            Intent intent = new Intent();
            SetResult(Result.Ok, intent);

            Finish();
        }
    }
}