
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

        IO.MyDatabase database;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            database = new IO.MyDatabase(Activity.Assets);
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
                AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                alert.SetTitle("Delete formular");
                alert.SetMessage("Do you want to delete this formular?");
                alert.SetNegativeButton("Yes", (senderAlert, args) => {
                    this.funcs.Remove(this.funcSelected);
                    this.values.Remove(this.funcSelected);
                    Calc.Data data = database.GetItemAsync(shapeName + "Function").Result;
                    data.value = "";
                    foreach (KeyValuePair<string, List<string>> i in funcs)
                    {
                        foreach (string j in i.Value)
                        {
                            data.value += (j + "\n");
                        }
                    }
                    data.value = data.value.Remove(data.value.Length - 1);
                    database.SaveItemAsync(data);
                    spinner.Adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItem1, values.ToArray());
                    listView.Adapter = new ListViewAdapter(values);
                    Toast.MakeText(Activity, "Deleted!", ToastLength.Short).Show();
                });
                alert.SetPositiveButton("No", (senderAlert, args) => {

                });
                Dialog dialog = alert.Create();
                dialog.Show();
            };
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            this.funcSelected = this.values[e.Position];
        }

        private void setFuncs()
        {
            List<List<string>> tmp = IO.FuncReader.getInstance().getFunc(database, shapeName);
            funcs = new Dictionary<string, List<string>>();
            values = new List<string>();
            if (tmp == null){
                return;
            }
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
