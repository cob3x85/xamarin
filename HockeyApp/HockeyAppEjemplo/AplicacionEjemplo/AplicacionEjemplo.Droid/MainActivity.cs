using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HockeyApp.Android;

namespace AplicacionEjemplo.Droid
{
  [Activity(Label = "AplicacionEjemplo", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
  {
    protected override void OnCreate(Bundle bundle)
    {
      TabLayoutResource = Resource.Layout.Tabbar;
      ToolbarResource = Resource.Layout.Toolbar;

      base.OnCreate(bundle);

      CrashManager.Register(this, "f0273e46db8b44679255002c5d652db5");

      global::Xamarin.Forms.Forms.Init(this, bundle);
      LoadApplication(new App());
    }
  }
}

