
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
    public class EditShape : Fragment
    {
        private Button btnAdd;
        private Button btnDelete;
        private Button btnFinish;
        private LinearLayout linearLayout;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.EditShape_Layout, container, false);

            initBtnAdd(view);
            initBtnDelete(view);
            initBtnFinish(view);
            initLinearLayout(view);

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void initLinearLayout(View view)
        {
            linearLayout = view.FindViewById<LinearLayout>(Resource.Id.editShapeControlLayout);
            ListView listView = new ListView(Activity);
            listView.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            listView.Adapter = new ListViewAdapter(IO.ShapeReader.getInstance().getShapes(Activity.Assets, "Shape.txt"));
            linearLayout.AddView(listView);
        }

        private void initBtnFinish(View view)
        {
            btnFinish = view.FindViewById<Button>(Resource.Id.btnEditShapeFinish);
            btnFinish.Click += delegate {

                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new StartEdit());
                fragmentTransaction.Commit();
            };
        }

        private void initBtnDelete(View view)
        {
            btnDelete = view.FindViewById<Button>(Resource.Id.btnEditShapeDelete);
            btnDelete.Click += delegate {
                linearLayout.RemoveAllViews();

            };
        }

        private void initBtnAdd(View view)
        {
            btnAdd = view.FindViewById<Button>(Resource.Id.btnEditShapeAdd);
            btnAdd.Click += delegate {

                linearLayout.RemoveAllViews();

            };
        }
    }
}
