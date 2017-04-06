using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using weblayer.embarcador.core.Model;

namespace weblayer.embarcador.android.Adapters
{
    public class Adapter_NotaFiscal_ListView : BaseAdapter<NotaFiscal>
    {
        public List<NotaFiscal> mItems;
        public Context mContext;

        public Adapter_NotaFiscal_ListView(Context context, List<NotaFiscal> items)
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

        public override NotaFiscal this[int position]
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.Adapter_NotaFiscal_ListView, null, false);


            row.FindViewById<TextView>(Resource.Id.txtCliente).Text = mItems[position].ds_cliente;
            row.FindViewById<TextView>(Resource.Id.txtNumNota).Text = "Nota/Série: " + mItems[position].ds_numeronota + "/" + mItems[position].ds_serienota;
            row.FindViewById<TextView>(Resource.Id.txtValor).Text = "Valor: " + mItems[position].ds_valor;

            if (mItems[position].dt_entrega.HasValue)
                row.FindViewById<TextView>(Resource.Id.txtDataEntrega).Text = "Entrega: " + mItems[position].dt_entrega.Value.ToString("dd/MM/yyyy");
            else
                row.FindViewById<TextView>(Resource.Id.txtDataEntrega).Text = "Entrega: Pendente";



            return row;

        }
    }
}