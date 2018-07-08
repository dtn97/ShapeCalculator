
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
    public class MainMenu : Fragment
    {
        Button btnStart;
        Button btnAbout;
        Button btnExit;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.MainMenu_Layout, container, false);

            initBtnStart(view);
            initBtnAbout(view);
            initBtnExit(view);

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void initBtnExit(View view)
        {
            btnExit = view.FindViewById<Button>(Resource.Id.btnMainMenuExit);
            btnExit.Click += delegate {
                (Activity).Finish();
            };
        }

        private void initBtnAbout(View view)
        {
            btnAbout = view.FindViewById<Button>(Resource.Id.btnMainMenuAbout);
            btnAbout.Click += delegate {
                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new About());
                fragmentTransaction.Commit();
            };
        }

        private void initBtnStart(View view){
            btnStart = view.FindViewById<Button>(Resource.Id.btnMainMenuStart);
            btnStart.Click += delegate {
                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new Start());
                fragmentTransaction.Commit();
            };
        }
    }
}
