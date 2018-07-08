
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
    public class StartView : Fragment
    {
        private string shapeSelected;
        private string typeSelected;

        private List<string> vars;
        private List<string> shapes;
        private List<string> types;

        Button btnViewResult;
        ListView lvResult;

        Spinner spinner1;
        Spinner spinner2;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            shapes = IO.ShapeReader.getInstance().getShapes(Activity.Assets, "Shape.txt");
            types = new List<string>();
            types.Add("Variable");
            types.Add("Formula");

            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.StartView_Layout, container, false);

            initBtnViewResult(view);
            initLvResult(view);
            initSpinners(view);

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void initSpinners(View view)
        {
            spinner1 = view.FindViewById<Spinner>(Resource.Id.spnStartViewShape);
            spinner1.Adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, shapes.ToArray());
            spinner1.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner1_ItemSelected);

            spinner2 = view.FindViewById<Spinner>(Resource.Id.spnStartViewType);
            spinner2.Adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, types.ToArray());
            spinner2.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner2_ItemSelected);
        }

        private void spinner1_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e){
            
            this.shapeSelected = shapes[e.Position];
        }

        private void spinner2_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            this.typeSelected = types[e.Position];
        }

        private void initBtnViewResult(View view)
        {
            btnViewResult = view.FindViewById<Button>(Resource.Id.btnStartViewResult);
            btnViewResult.Click += delegate {
                if (this.typeSelected.Equals("Variable")){
                    List<string> listVars = IO.VarReader.getInstance().getVariables(Activity.Assets, this.shapeSelected);
                    lvResult.Adapter = new ListViewAdapter(listVars);
                }
                else{
                    lvResult.Adapter = new ListViewAdapter(IO.FuncReader.getInstance().getFunctions(Activity.Assets, this.shapeSelected));
                }

            };
        }

        private void initLvResult(View view)
        {
            lvResult = view.FindViewById<ListView>(Resource.Id.lvStartViewResult);
        }
    }
}
