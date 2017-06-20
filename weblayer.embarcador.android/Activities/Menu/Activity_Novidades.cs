using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace weblayer.embarcador.android.Activities
{
    [Activity(Label = "Novidades")]
    public class Activity_Novidades : Activity_Base
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        private TextView txtNovidades;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_Novidades;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Menu.RemoveItem(Resource.Id.action_filtrar);
            toolbar.Menu.RemoveItem(Resource.Id.action_contato);

            FindViews();
            BindData();
        }

        private void FindViews()
        {
            txtNovidades = FindViewById<TextView>(Resource.Id.txtNovidades);
        }

        private void BindData()
        {
            txtNovidades.Text = Novidades();
        }

        private string Novidades()
        {
            string Novidades;

            Novidades = " 1.2 (19/06/2017):"
                                     + "\n  [Melhorias] Adicionado o menmu de  Contato";


            Novidades = Novidades  + " \n\n  1.1 (10/05/2017):"
                                     + "\n  [Melhorias] Correção na exibição no gráfico de cenário de entrega";

            Novidades = Novidades  + " \n\n 1.0 (23/01/2017):"
                                     + "\n    [Novo] Implementação do leitor de código de barras"
                                     + "\n    [Melhorias] Atualização da interface";


            return Novidades;
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