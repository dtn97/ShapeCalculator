
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
    public class StartEdit : Fragment
    {
        private Button btnAdd;
        private Button btnDelete;
        private ListView listView;
        private EditText editText;

        private List<string> shapes;
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

            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.StartEdit_Layout, container, false);

            initBtnBack(view);
            initBtnAdd(view);
            initBtnDelete(view);

            listView = view.FindViewById<ListView>(Resource.Id.startEditListView);
            listView.Adapter = new ListViewAdapter(shapes);

            editText = view.FindViewById<EditText>(Resource.Id.edtstartEdit);

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void initBtnDelete(View view)
        {
            btnDelete = view.FindViewById<Button>(Resource.Id.btnStartEditDelete);
            btnDelete.Click += delegate {
                AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                alert.SetTitle("Delete Shape");
                alert.SetMessage("Do you want to delete shape?");
                alert.SetNegativeButton("Yes", (senderAlert, args) => {
                    string shape = editText.Text.ToString();
                    if (shape.Equals("") || !shapes.Contains(shape))
                    {
                        editText.Text = "";
                        return;
                    }
                    shapes.Remove(shape);
                    listView.Adapter = new ListViewAdapter(shapes);
                    editText.Text = "";
                    string tmp = "";
                    foreach (string i in shapes)
                    {
                        tmp += (i + "\n");
                    }
                    if (tmp.Length > 0 && tmp[tmp.Length - 1] == '\n')
                    {
                        tmp = tmp.Remove(tmp.Length - 1);
                    }
                    Calc.Data data = database.GetItemAsync("Shape").Result;
                    data.value = tmp;
                    database.SaveItemAsync(data);

                    data = database.GetItemAsync(shape + "Variable").Result;
                    database.DeleteItemAsync(data);

                    data = database.GetItemAsync(shape + "Function").Result;
                    database.DeleteItemAsync(data);
                    Toast.MakeText(Activity, "Deleted!", ToastLength.Short).Show();
                });
                alert.SetPositiveButton("No", (senderAlert, args) => {

                });
                Dialog dialog = alert.Create();
                dialog.Show();


            };
        }

        private void initBtnAdd(View view)
        {
            btnAdd = view.FindViewById<Button>(Resource.Id.btnStartEditAdd);
            btnAdd.Click += delegate {
                string shape = editText.Text.ToString();
                if (shape.Equals("")){
                    return;
                }
                shapes.Add(shape);
                listView.Adapter = new ListViewAdapter(shapes);
                Calc.Data data = database.GetItemAsync("Shape").Result;
                data.value += ("\n" + shape);
                database.SaveItemAsync(data);
                editText.Text = "";
                Toast.MakeText(Activity, "Successed!", ToastLength.Short).Show();
            };
        }

        private void initBtnBack(View view)
        {
            Button btnMenu = view.FindViewById<Button>(Resource.Id.btnStartEditMenu);
            btnMenu.Click += delegate {

                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new MainMenu());
                fragmentTransaction.Commit();
            };

            Button btnBack = view.FindViewById<Button>(Resource.Id.btnStartEditBack);
            btnBack.Click += delegate {
                FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.mainLayout, new Start());
                fragmentTransaction.Commit();
            };
        }
    }
}
