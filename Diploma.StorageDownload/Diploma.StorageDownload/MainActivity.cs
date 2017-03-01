using Android.App;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using XamarinDiplomado.Participants.Startup;

namespace Diploma.StorageDownload
{
  [Activity(Label = "Download Image", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {

    ImageView imagen;
    Button btnDescargar;
    GoogleMap googleMap;
    MapView mapView;
    double latitud, longitud;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      // Set our view from the "main" layout resource
      SetContentView (Resource.Layout.Main);

      //Get controls into variables
      imagen = FindViewById<ImageView>(Resource.Id.imagen);
      btnDescargar = FindViewById<Button>(Resource.Id.btnDescargar);


      Startup startup = new Startup("Carlos Ortiz Bravo", "cob3x85@outlook.com", 3, 1);
      startup.Init();
    }
  }
}

