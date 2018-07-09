
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
    public class AddVariable : Fragment
    {
        
        private Button btnAdd;
        private ListView listView;
        private EditText editText;

        private string shapeName;
        IO.MyDatabase database;
        private List<string> vars;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            shapeName = Arguments.GetString("shape");
            database = new IO.MyDatabase(Activity.Assets);
            vars = IO.VarReader.getInstance().getVars(database, this.shapeName);

            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.AddVariable_Layout, container, false);

            initBtnBack(view);
            initBtnAdd(view);
            editText = view.FindViewById<EditText>(Resource.Id.edtAddVarInput);
            listView = view.FindViewById<ListView>(Resource.Id.lvAddVarInfo);
            listView.Adapter = new ListViewAdapter(vars);

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void initBtnAdd(View view)
        {
            btnAdd = view.FindViewById<Button>(Resource.Id.btnAddVar);
            btnAdd.Click += delegate {
                if (vars.Contains(editText.Text) || editText.Text.ToString().Equals("")){
                    return;
                }
                vars.Add(editText.Text);
                Calc.Data data = database.GetItemAsync(shapeName + "Variable").Result;
                data.value += ("\n" + editText.Text);
                database.SaveItemAsync(data);
                editText.Text = "";
                listView.Adapter = new ListViewAdapter(vars);
            };
        }

        private void initBtnBack(View view)
        {
            Button button1 = view.FindViewById<Button>(Resource.Id.btnAddVarBack);
            button1.Click += delegate {

                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new StartView());
                fragmentTransaction.Commit();
            };

            Button button2 = view.FindViewById<Button>(Resource.Id.btnAddVarMenu);
            button2.Click += delegate {

                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new MainMenu());
                fragmentTransaction.Commit();
            };
        }
    }
}
