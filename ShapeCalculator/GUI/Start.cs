
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
            initBtnReset(view);

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void initBtnReset(View view)
        {
            Button button = view.FindViewById<Button>(Resource.Id.btnStartReset);
            button.Click += delegate {
                AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                alert.SetTitle("Reset");
                alert.SetMessage("Reset Successed");
                alert.SetNegativeButton("Yes", (senderAlert, args) => {
                    IO.MyDatabase database = new IO.MyDatabase(Activity.Assets);
                    database.reset(Activity.Assets);
                });
                alert.SetPositiveButton("No", (senderAlert, args) => {
                    
                });
                Dialog dialog = alert.Create();
                dialog.Show();
            };
        }

        private void initBtnCalc(View view)
        {
            btnCalc = view.FindViewById<Button>(Resource.Id.btnStartCalc);
            btnCalc.Click += delegate {
                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new StartCalc());
                fragmentTransaction.Commit();

            };
        }

        private void initBtnView(View view)
        {
            btnView = view.FindViewById<Button>(Resource.Id.btnStartView);
            btnView.Click += delegate {
                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new StartView());
                fragmentTransaction.Commit();
            };
        }

        private void initBtnEdit(View view)
        {
            btnEdit = view.FindViewById<Button>(Resource.Id.btnStartEdit);
            btnEdit.Click += delegate {
                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new StartEdit());
                fragmentTransaction.Commit();
            };
        }
    }
}
