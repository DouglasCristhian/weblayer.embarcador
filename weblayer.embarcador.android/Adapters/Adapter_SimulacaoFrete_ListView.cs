using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using weblayer.embarcador.core.Model;

namespace weblayer.embarcador.android.Adapters
{
    public class Adapter_SimulacaoFrete_ListView : BaseAdapter<SimulacaoFrete>
    {
        public List<SimulacaoFrete> mItems;
        public Context mContext;

        public Adapter_SimulacaoFrete_ListView(Context context, List<SimulacaoFrete> items)
        {
            mItems = items;
            mContext = context;
        }

        public override int Count
        {
            get
            {
                return mItems.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override SimulacaoFrete this[int position]
        {
            get
            {
                return mItems[position];
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.Adapter_SimulacaoFrete_ListView, null, false);

            row.FindViewById<TextView>(Resource.Id.txtNomeTransportadora).Text = mItems[position].ds_transportadora;
            row.FindViewById<TextView>(Resource.Id.txtFrete).Text = "Frete: R$" + mItems[position].vl_frete;
            row.FindViewById<TextView>(Resource.Id.txtFreteImposto).Text = "Frete + Imposto: R$" + mItems[position].vl_frete_imposto;

            return row;
        }
    }
}