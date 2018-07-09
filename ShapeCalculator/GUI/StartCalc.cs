
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
    public class StartCalc : Fragment
    {
        private string shapeSelected;
        private string varSelected;
        private List<string> shapes;
        private List<string> vars;

        private Spinner spinner1;
        private Spinner spinner2;
        private EditText editText;
        private Button btnEnter;
        private Button btnCalculate;
        private ListView lvResult;

        Dictionary<string, double> inputVars;
        List<string> varEntered;

        IO.MyDatabase database;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            database = new IO.MyDatabase(Activity.Assets);
            shapes = IO.ShapeReader.getInstance().getShapes(database);
            //vars = IO.VarReader.getInstance().getVariables(Activity.Assets, shapes[0]);

            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.StartCalc_Layout, container, false);

            initSpinners(view);
            initEditText(view);
            initBtnEnter(view);
            initBtnCalculate(view);
            initListViewResult(view);

            initBtnBackMenu(view);

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void initBtnBackMenu(View view)
        {
            Button button1 = view.FindViewById<Button>(Resource.Id.btnCalcBack);
            button1.Click += delegate {

                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new Start());
                fragmentTransaction.Commit();
            };

            Button button2 = view.FindViewById<Button>(Resource.Id.btnCalcMenu);
            button2.Click += delegate {

                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new MainMenu());
                fragmentTransaction.Commit();
            };
        }

        private void initListViewResult(View view)
        {
            lvResult = view.FindViewById<ListView>(Resource.Id.lvStartCalcResult);
        }

        private void initBtnCalculate(View view)
        {
            btnCalculate = view.FindViewById<Button>(Resource.Id.btnStartCalcCalc);
            btnCalculate.Click += delegate {
                if (vars.Count == 0){
                    Toast.MakeText(Activity, "Calculate Fail!", ToastLength.Short).Show();
                    return;
                }
                while (vars.Count > 0){
                    inputVars.Add(vars[0], -1);
                    vars.RemoveAt(0);
                }
                StartResult myFragment = new StartResult();
                FragmentTransaction ft = this.FragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.mainLayout, myFragment);
                Bundle args = new Bundle();
                args.PutString("type", shapeSelected);
                foreach (KeyValuePair<string, double> i in inputVars)
                {
                    args.PutString(i.Key, i.Value.ToString());
                }
                myFragment.Arguments = args;
                ft.Commit();
            };
        }

        private void initBtnEnter(View view)
        {
            btnEnter = view.FindViewById<Button>(Resource.Id.btnStartCalcEnter);
            btnEnter.Click += delegate {
                if (editText.Text.ToString().Equals("") || this.varSelected == null){
                    Toast.MakeText(Activity, "Fail!", ToastLength.Short).Show();
                    return;
                }
                vars.Remove(this.varSelected);
                double value = double.Parse(editText.Text);
                inputVars.Add(this.varSelected, value);
                varEntered.Add(this.varSelected + " = " + value.ToString());
                lvResult.Adapter = new ListViewAdapter(varEntered);
                editText.Text = "";
                spinner2.Adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, vars.ToArray());
            };
        }

        private void initEditText(View view)
        {
            editText = view.FindViewById<EditText>(Resource.Id.txtStartCalcVar);
        }

        private void initSpinners(View view)
        {
            spinner1 = view.FindViewById<Spinner>(Resource.Id.spnStartCalcShape);
            spinner1.Adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, shapes.ToArray());
            spinner1.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner1_ItemSelected);
            spinner2 = view.FindViewById<Spinner>(Resource.Id.spnStartCalcVar);
            spinner2.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner2_ItemSelected);
        }

        private void spinner2_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            this.varSelected = this.vars[e.Position];
        }

        private void spinner1_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            this.shapeSelected = this.shapes[e.Position];
            this.vars = IO.VarReader.getInstance().getVars(database, this.shapeSelected);
            if (vars == null){
                vars = new List<string>();
            }
            spinner2.Adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, vars.ToArray());
            lvResult.Adapter = null;
            inputVars = new Dictionary<string, double>();
            varEntered = new List<string>();
        }
    }
}
