
using System;
using System.Collections.Generic;
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
    public class StartEdit : Fragment
    {
        private Button btnShape;
        private Button btnVar;
        private Button btnFunc;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.StartEdit_Layout, container, false);

            btnShape = view.FindViewById<Button>(Resource.Id.btnStartEditShape);
            btnShape.Click += delegate {
                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new EditShape());
                fragmentTransaction.Commit();

            };

            btnVar = view.FindViewById<Button>(Resource.Id.btnStartEditVariable);
            btnVar.Click += delegate {
            

            };

            btnFunc = view.FindViewById<Button>(Resource.Id.btnStartEditFormula);
            btnFunc.Click += delegate {
            

            };

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}
