using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using weblayer.embarcador.core.Model;

namespace weblayer.embarcador.android.Adapters
{
    [Activity(Label = "Adapter_Cidade_ListView")]
    public class Adapter_Cidade_ListView : BaseAdapter<Cidade>
    {
        public List<Cidade> mItems;
        public Context mContext;

        public Adapter_Cidade_ListView(Context context, List<Cidade> items)
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

        public override Cidade this[int position]
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.Adapter_Cidade_ListView, null, false);

            row.FindViewById<TextView>(Resource.Id.txtCidade).Text = mItems[position].ds_cidade;
            row.FindViewById<TextView>(Resource.Id.txtEstado).Text = mItems[position].ds_estado;

            return row;

        }
    }
}