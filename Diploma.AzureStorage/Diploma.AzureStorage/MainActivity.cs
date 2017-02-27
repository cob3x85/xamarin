using Android.App;
using Android.Widget;
using Android.OS;
using XamarinDiplomado.Participants.Startup;

namespace Diploma.AzureStorage
{
  [Activity(Label = "Diploma.AzureStorage", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      // Set our view from the "main" layout resource
      // SetContentView (Resource.Layout.Main);
      Startup startup = new Startup("Carlos Ortiz Bravo", "cob3x85@outlook.com", 1, 1);
      startup.Init();
    }
  }
}

