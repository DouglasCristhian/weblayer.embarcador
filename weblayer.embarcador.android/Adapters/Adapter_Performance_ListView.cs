using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using weblayer.embarcador.core.Model;

namespace weblayer.embarcador.android.Adapters
{
    public class Adapter_Performance_ListView : BaseAdapter<Performance>
    {
        public List<Performance> mItems;
        public Context mContext;

        public Adapter_Performance_ListView(Context context, List<Performance> items)
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

        public override Performance this[int position]
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.Adapter_Performance_ListView, null, false);

            row.FindViewById<TextView>(Resource.Id.txtPerformance).Text = mItems[position].ds_percentual_performance;
            row.FindViewById<TextView>(Resource.Id.txtNomeTransportadora).Text = mItems[position].ds_titulo;

            if (mItems[position].ds_cor == "VERDE")
                row.FindViewById<TextView>(Resource.Id.txtPerformance).SetBackgroundResource(Resource.Drawable.PerformanceVerde);

            if (mItems[position].ds_cor == "AMARELO")
                row.FindViewById<TextView>(Resource.Id.txtPerformance).SetBackgroundResource(Resource.Drawable.PerformanceAmarelo);

            if (mItems[position].ds_cor == "VERMELHO")
                row.FindViewById<TextView>(Resource.Id.txtPerformance).SetBackgroundResource(Resource.Drawable.PerformanceVermelho);



            return row;

        }
    }
}