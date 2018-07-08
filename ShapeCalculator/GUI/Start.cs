
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
    public class Start : Fragment
    {
        private Button btnCalc;
        private Button btnView;
        private Button btnEdit;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Contract.Ensures(Contract.Result<View>() != null);
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.Start_Layout, container, false);

            initBtnCalc(view);
            initBtnView(view);
            initBtnEdit(view);

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void initBtnCalc(View view)
        {
            btnCalc = view.FindViewById<Button>(Resource.Id.btnStartCalc);
            btnCalc.Click += delegate {
            

            };
        }

        private void initBtnView(View view)
        {
            btnView = view.FindViewById<Button>(Resource.Id.btnStartView);
            btnView.Click += delegate {
            

            };
        }

        private void initBtnEdit(View view)
        {
            btnEdit = view.FindViewById<Button>(Resource.Id.btnStartEdit);
            btnEdit.Click += delegate {
            

            };
        }
    }
}
