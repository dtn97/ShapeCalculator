using Android.App;
using Android.Widget;
using Android.OS;

namespace ShapeCalculator
{
    [Activity(Label = "ShapeCalculator", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            FragmentTransaction fragmentTransaction = this.FragmentManager.BeginTransaction();
            fragmentTransaction.Replace(Resource.Id.mainLayout, new MainMenu());
            fragmentTransaction.Commit();
        }
    }
}

