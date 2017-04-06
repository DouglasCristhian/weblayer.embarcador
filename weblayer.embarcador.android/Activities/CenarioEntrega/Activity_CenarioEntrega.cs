using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using weblayer.embarcador.core.BLL;
using weblayer.embarcador.core.Model;

namespace weblayer.embarcador.android.Activities
{
    [Activity(ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class Activity_CenarioEntrega : Activity_Base
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        private PlotView view;
        private string AnoSelecionado;
        private string MesSelecionado;
        private TextView txt;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_CenarioEntrega;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.InflateMenu(Resource.Menu.menu_toolbar);
            toolbar.Menu.RemoveItem(Resource.Id.action_sobre);
            toolbar.Menu.RemoveItem(Resource.Id.action_sair);

            Filtro_Spinner();
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

            string ano = prefs.GetString("PrefAnoCenarioEntregaString", DateTime.Now.Year.ToString());
            AnoSelecionado = ano;

            string mes = prefs.GetString("PrefMesCenarioEntrega", DateTime.Now.Month.ToString());
            MesSelecionado = mes;

            FindViews();
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
                    intent.SetClass(this, typeof(Activity_FiltrarCenarioEntrega));
                    StartActivityForResult(intent, 0);
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FindViews()
        {
            view = FindViewById<PlotView>(Resource.Id.plotView);
            txt = FindViewById<TextView>(Resource.Id.titulo);

            string DataFinal = (MesSelecionado + "/" + AnoSelecionado).ToString();
            this.Title = "Cenário de Entrega (" + DataFinal + ")";

            BindData();
        }

        //public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        //{
        //    base.OnConfigurationChanged(newConfig);

        //    if (newConfig.Orientation == Android.Content.Res.Orientation.Portrait)
        //    {
        //        view.Model = GraficoBarras(int.Parse(AnoSelecionado), int.Parse(MesSelecionado));
        //    }
        //    else if (newConfig.Orientation == Android.Content.Res.Orientation.Landscape)
        //    {
        //        view.Model = GraficoColunas(int.Parse(AnoSelecionado), int.Parse(MesSelecionado));
        //    }
        //}

        private void BindData()
        {
            view.Model = GraficoBarras(int.Parse(AnoSelecionado), int.Parse(MesSelecionado));

            //if (this.Resources.Configuration.Orientation == Android.Content.Res.Orientation.Portrait)
            //{
            //    view.Model = GraficoBarras(int.Parse(AnoSelecionado), int.Parse(MesSelecionado));
            //}
            //else if (this.Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape)
            //{
            //    view.Model = GraficoColunas(int.Parse(AnoSelecionado), int.Parse(MesSelecionado));
            //}
        }

        private PlotModel GraficoColunas(int ano, int mes)
        {
            var plotModel = new PlotModel()
            {
                LegendPlacement = LegendPlacement.Outside,
                IsLegendVisible = true,

            };

            plotModel.Title = "Cenário de Entrega";

            CenarioEntrega ent = new CenarioEntrega();
            CenarioEntregaManager manager = new CenarioEntregaManager();
            List<CenarioEntrega> list = manager.GetCenario2(ano, mes);

            List<CenarioEntrega> SortedList = list.OrderBy(o => o.nr_dias).ToList();
            //CenarioEntrega intMax = SortedList.Max();

            var categoryAxis1 = new CategoryAxis()
            {
                Title = "Período da Entrega",
                //AbsoluteMaximum = intMax.nr_dias,
                MaximumRange = 15
            };

            var columnSeries = new ColumnSeries()
            {
                FontSize = 15,
                TextColor = OxyColors.Black,
                ColumnWidth = 20,
            };

            for (int i = 0; i < SortedList.Count; i++)
            {
                if (SortedList[i].nr_dias < 0)
                {
                    categoryAxis1.Labels.Add((SortedList[i].nr_dias * -1).ToString() + " dias");
                    columnSeries.Items.Add(new ColumnItem(SortedList[i].nr_notas) { Color = OxyColors.Red });
                }
            }

            var columnSeries2 = new ColumnSeries()
            {
                FontSize = 25,
                TextColor = OxyColors.Black,
                LabelPlacement = LabelPlacement.Outside,
            };

            for (int i = 0; i < SortedList.Count; i++)
            {
                if (SortedList[i].nr_dias >= 0)
                {
                    categoryAxis1.Labels.Add(SortedList[i].nr_dias.ToString() + " dias");
                    columnSeries.Items.Add(new ColumnItem(SortedList[i].nr_notas) { Color = OxyColors.Green });
                }
            }

            plotModel.Axes.Add(categoryAxis1);
            plotModel.Series.Add(columnSeries);
            plotModel.Series.Add(columnSeries2);

            return plotModel;
        }

        private PlotModel GraficoBarras(int ano, int mes)
        {
            var plotModel = new PlotModel
            {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0
            };

            CenarioEntrega ent = new CenarioEntrega();
            CenarioEntregaManager manager = new CenarioEntregaManager();
            List<CenarioEntrega> list = manager.GetCenario2(ano, mes);

            List<CenarioEntrega> SortedList = list.OrderBy(o => -o.nr_dias).ToList();

            var barSeries = new BarSeries { Title = "Cenário de Entrega", StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };

            for (int i = 0; i < SortedList.Count; i++)
            {
                if (SortedList[i].nr_dias >= 0)
                {
                    categoryAxis.Labels.Add((SortedList[i].nr_dias * 1).ToString() + " dias");
                    barSeries.Items.Add(new BarItem(SortedList[i].nr_notas) { Color = OxyColors.Green });
                }
            }

            int valorMaximoX = SortedList.Max(r => r.nr_notas) + 10;

            for (int i = 0; i < SortedList.Count; i++)
            {
                if (SortedList[i].nr_dias < 0)
                {
                    categoryAxis.Labels.Add((SortedList[i].nr_dias * -1).ToString() + " dias");
                    barSeries.Items.Add(new BarItem(SortedList[i].nr_notas) { Color = OxyColors.Red });
                }
            }

            categoryAxis.AbsoluteMinimum = -1;
            categoryAxis.AbsoluteMaximum = SortedList.Count;

            var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0, AbsoluteMaximum = valorMaximoX };
            plotModel.Series.Add(barSeries);
            plotModel.Axes.Add(categoryAxis);
            plotModel.Axes.Add(valueAxis);

            return plotModel;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            Filtro_Spinner();

        }
    }
}