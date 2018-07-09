
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

        private List<string> shapes;
        private List<string> types;

        private ListView lvResult;

        private Spinner spinner1;
        private Spinner spinner2;

        private Button btnAdd;
        private Button btnDelete;

        IO.MyDatabase database;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            database = new IO.MyDatabase(Activity.Assets);
            Calc.Data data = database.GetItemAsync("Shape").Result;
            shapes = new List<string>(data.value.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));

            this.typeSelected = "Variable";
            //shapes = IO.ShapeReader.getInstance().getShapes(Activity.Assets, "Shape.txt");
            types = new List<string>();
            types.Add("Variable");
            types.Add("Function");

            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.StartView_Layout, container, false);

            initLvResult(view);
            initSpinners(view);
            initBtnAdd(view);
            initBtnDelete(view);
            InitBtnBack(view);

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void InitBtnBack(View view)
        {
            Button button1 = view.FindViewById<Button>(Resource.Id.btnStartViewBack);
            button1.Click += delegate {

                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new Start());
                fragmentTransaction.Commit();
            };

            Button button2 = view.FindViewById<Button>(Resource.Id.btnStartViewMenu);
            button2.Click += delegate {

                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new MainMenu());
                fragmentTransaction.Commit();
            };
        }

        private void initBtnDelete(View view)
        {
            btnDelete = view.FindViewById<Button>(Resource.Id.btnStartViewDelete);
            btnDelete.Click += delegate {
                Fragment fragment;
                if (this.typeSelected.Equals("Variable")){
                    fragment = new DeleteVariable();
                }
                else{
                    fragment = new DeleteFunc();
                }
                FragmentTransaction ft = this.FragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.mainLayout, fragment);
                Bundle args = new Bundle();
                args.PutString("shape", this.shapeSelected);
                fragment.Arguments = args;
                ft.Commit();
            };
        }

        private void initBtnAdd(View view)
        {
            btnAdd = view.FindViewById<Button>(Resource.Id.btnStartViewAdd);
            btnAdd.Click += delegate {
                Fragment fragment;
                if (this.typeSelected.Equals("Variable"))
                {
                    fragment = new AddVariable();
                }
                else
                {
                    fragment = new AddFunction();
                }
                FragmentTransaction ft = this.FragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.mainLayout, fragment);
                Bundle args = new Bundle();
                args.PutString("shape", this.shapeSelected);
                fragment.Arguments = args;
                ft.Commit();

            };
        }

        private void initSpinners(View view)
        {
            /*
            if (Arguments.ContainsKey("shape")){
                string name = Arguments.GetString("shape");
                int index = shapes.IndexOf(name);
                shapes[index] = shapes[0];
                shapes[0] = name;
                types[0] = "Function";
                types[1] = "Variable";
            }
            */

            spinner1 = view.FindViewById<Spinner>(Resource.Id.spnStartViewShape);
            spinner1.Adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, shapes.ToArray());
            spinner1.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner1_ItemSelected);

            spinner2 = view.FindViewById<Spinner>(Resource.Id.spnStartViewType);
            spinner2.Adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, types.ToArray());
            spinner2.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner2_ItemSelected);

        }

        private void spinner1_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e){
            
            this.shapeSelected = shapes[e.Position];
            updateListView();
        }

        private void spinner2_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            this.typeSelected = types[e.Position];
            updateListView();
        }

        private void updateListView()
        {
            if (this.typeSelected.Equals("Variable"))
            {
                Calc.Data data = database.GetItemAsync(this.shapeSelected + this.typeSelected).Result;
                List<string> listVars = new List<string>(data.value.Split(new String[]{"\n"}, StringSplitOptions.RemoveEmptyEntries));
                lvResult.Adapter = new ListViewAdapter(listVars);
            }
            else
            {
                lvResult.Adapter = new ListViewAdapter(IO.FuncReader.getInstance().getFunctions(database, this.shapeSelected));
            }
        }

        private void initLvResult(View view)
        {
            lvResult = view.FindViewById<ListView>(Resource.Id.lvStartViewResult);
        }
    }
}
