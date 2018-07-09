
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
    public class DeleteFunc : Fragment
    {
        private string shapeName;
        private Dictionary<string, List<string>> funcs;
        private List<string> values;
        private string funcSelected;

        private ListView listView;
        private Spinner spinner;
        private Button btnDelete;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            shapeName = Arguments.GetString("shape");
            setFuncs();
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.DeleteInfo_Layout, container, false);
            listView = view.FindViewById<ListView>(Resource.Id.lvDeleteInfo);
            listView.Adapter = new ListViewAdapter(values);

            spinner = view.FindViewById<Spinner>(Resource.Id.spnDeleteInfo);
            spinner.Adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItem1, values.ToArray());
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            initBtnBack(view);
            initBtnDelete(view);

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void initBtnDelete(View view)
        {
            btnDelete = view.FindViewById<Button>(Resource.Id.btnDeleteInfo);
            btnDelete.Click += delegate {



            };
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            this.funcSelected = this.values[e.Position];
        }

        private void setFuncs()
        {
            List<List<string>> tmp = IO.FuncReader.getInstance().getFunc(Activity.Assets, shapeName);
            funcs = new Dictionary<string, List<string>>();
            values = new List<string>();
            foreach(List<string> i in tmp){
                funcs.Add(i[1], i);
                values.Add(i[1]);
            }
        }

        protected virtual void initBtnBack(View view)
        {
            Button button1 = view.FindViewById<Button>(Resource.Id.btnDeleteInfoBack);
            button1.Click += delegate {

                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new StartView());
                fragmentTransaction.Commit();
            };

            Button button2 = view.FindViewById<Button>(Resource.Id.btnDeleteInfoMenu);
            button2.Click += delegate {

                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new MainMenu());
                fragmentTransaction.Commit();
            };
        }
    }
}
