
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
        LinearLayout linearLayout;
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

            linearLayout = view.FindViewById<LinearLayout>(Resource.Id.mainMenuLayout);

            initBtnStart();
            initBtnAbout();
            initBtnExit();
            addButton();

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void addButton()
        {
            linearLayout.AddView(btnStart);
            linearLayout.AddView(btnAbout);
            linearLayout.AddView(btnExit);
        }

        private void initBtnExit()
        {
            btnExit = new Button(Activity);
            btnExit.Text = "Exit";
            btnExit.Click += delegate {
                (Activity).Finish();
            };
        }

        private void initBtnAbout()
        {
            btnAbout = new Button(Activity);
            btnAbout.Text = "About";
            btnAbout.Click += delegate {
                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new About());
                fragmentTransaction.Commit();
            };
        }

        private void initBtnStart(){
            btnStart = new Button(Activity);
            btnStart.Text = "Start";
            btnStart.Click += delegate {
                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new Start());
                fragmentTransaction.Commit();
            };
        }
    }
}
