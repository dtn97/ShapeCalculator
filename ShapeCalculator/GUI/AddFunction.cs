
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
    public class AddFunction : Fragment
    {
        Button btnAdd;
        EditText edtTarget;
        EditText edtVar;
        EditText edtFunc;

        private HashSet<string> vars;
        IO.MyDatabase database;
        private string shapeName;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            shapeName = Arguments.GetString("shape");
            database = new IO.MyDatabase(Activity.Assets);
            vars = new HashSet<string>(IO.VarReader.getInstance().getVars(database, shapeName));
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.AddFunction_Layout, container, false);

            edtTarget = view.FindViewById<EditText>(Resource.Id.edtTarget);
            edtVar = view.FindViewById<EditText>(Resource.Id.edtVariable);
            edtFunc = view.FindViewById<EditText>(Resource.Id.edtFormula);
            initBtnAdd(view);

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void initBtnAdd(View view)
        {
            btnAdd = view.FindViewById<Button>(Resource.Id.btnAddFunction);
            btnAdd.Click += delegate {
                if (edtVar.Text.ToString().Equals("") || edtFunc.Text.ToString().Equals("") || edtTarget.Text.ToString().Equals("")){
                    callBack();
                    return;
                }
                List<string> variable = new List<string>(edtVar.Text.ToString().Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries));
                variable.Add(edtTarget.Text);
                foreach(string i in variable){
                    if (!vars.Contains(i)){
                        callBack();
                        return;
                    }
                }
                string name = edtTarget.Text + " = " + edtFunc.Text;
                Calc.FormulaNode formulaNode = new Calc.FormulaNode(name, edtTarget.Text, edtFunc.Text, edtVar.Text, false);
                string res = formulaNode.toStringFunc();
                res = res.Remove(res.Length - 1);
                Calc.Data data = database.GetItemAsync(shapeName + "Function").Result;

                if (data == null){
                    data = new Calc.Data();
                    data.name = shapeName + "Function";
                    data.value = res;
                }
                else
                    data.value += ("\n" + res);
                database.SaveItemAsync(data);
                callBack();
            };
        }

        private void callBack()
        {
            Fragment fragment = new StartView();
            FragmentTransaction ft = this.FragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.mainLayout, fragment);
            ft.Commit();
        }
    }
}
