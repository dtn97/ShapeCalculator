using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;

namespace ShapeCalculator
{
    public class ListViewAdapter : BaseAdapter<string>
    {
        private List<string> value;
        public ListViewAdapter(List<string> value)
        {
            this.value = value;
        }

        public override string this[int position] => value[position];

        public override int Count => value.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ListViewNode_Layout, parent, false);

                TextView textView = view.FindViewById<TextView>(Resource.Id.txtNode);
                view.Tag = new ViewHolder() { myTextView = textView };
            }

            var holder = (ViewHolder)view.Tag;
            holder.myTextView.Text = value[position];
            return view;
        }

        internal class ViewHolder : Java.Lang.Object
        {
            public TextView myTextView { get; set; }
        }
    }
}
