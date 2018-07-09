
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ShapeCalculator
{
    public class About : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Contract.Ensures(Contract.Result<View>() != null);
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.About_Layout, container, false);
            ListView listView = view.FindViewById<ListView>(Resource.Id.aboutListView);
            listView.Adapter = new ListViewAdapter(IO.AboutReader.getInstance().getNames(Activity.Assets));
            Button btnBack = view.FindViewById<Button>(Resource.Id.btnAboutBack);
            btnBack.Click += delegate {
                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new MainMenu());
                fragmentTransaction.Commit();
            };
            Button btnMenu = view.FindViewById<Button>(Resource.Id.btnAboutMenu);
            btnMenu.Click += delegate {
                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new MainMenu());
                fragmentTransaction.Commit();
            };
            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}
