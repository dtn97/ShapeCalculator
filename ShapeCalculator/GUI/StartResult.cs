
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
    public class StartResult : Fragment
    {

        private Button btnBack;
        private Button btnMenu;
        private ListView resultView;

        string type;
        List<List<string>> funcs;
        Dictionary<string, double> vars;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.StartResult_Layout, container, false);

            initBtnBack(view);
            initBtnMenu(view);
            initResultView(view);

            readInput();
            Calc.Process process = new Calc.Process();
            process.setFunctions(funcs);
            process.setVariables(vars);
            List<string> res = process.run();
            resultView.Adapter = new ListViewAdapter(res);

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void readInput()
        {
            type = Arguments.GetString("type");

            funcs = IO.FuncReader.getInstance().getFunc(Activity.Assets, type);
            vars = IO.VarReader.getInstance().getVars(Activity.Assets, type);
            List<string> tmp = new List<string>();
            foreach (KeyValuePair<string, double> i in vars)
            {
                tmp.Add(i.Key);
            }
            vars = new Dictionary<string, double>();
            foreach (string i in tmp)
            {
                vars.Add(i, double.Parse(Arguments.GetString(i)));
            }
        }

        private void initResultView(View view)
        {
            resultView = view.FindViewById<ListView>(Resource.Id.startResultListView);
        }

        private void initBtnMenu(View view)
        {
            btnMenu = view.FindViewById<Button>(Resource.Id.btnStartResultMenu);
            btnMenu.Click += delegate {
            
                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new MainMenu());
                fragmentTransaction.Commit();
            };
        }

        private void initBtnBack(View view)
        {
            btnBack = view.FindViewById<Button>(Resource.Id.btnStartResultBack);
            btnBack.Click += delegate {
                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new StartCalc());
                fragmentTransaction.Commit();

            };
        }
    }
}
