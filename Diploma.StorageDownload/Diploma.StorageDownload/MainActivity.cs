using Android.App;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using XamarinDiplomado.Participants.Startup;
using System.IO;

namespace Diploma.StorageDownload
{
  [Activity(Label = "Download Image", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity, IOnMapReadyCallback
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
      SetContentView(Resource.Layout.Main);
      Startup startup = new Startup("Carlos Ortiz Bravo", "cob3x85@outlook.com", 3, 1);
      startup.Init();

      //Get controls into variables
      imagen = FindViewById<ImageView>(Resource.Id.imagen);
      btnDescargar = FindViewById<Button>(Resource.Id.btnDescargar);

      btnDescargar.Click += async delegate
      {
        try
        {
          string carpeta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
          string archivoLocal = "Foto.jpg";
          string ruta = System.IO.Path.Combine(carpeta, archivoLocal);

          CloudStorageAccount cuentaAlmacenamiento = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=cobxamarin;AccountKey=wWWEUzzSVVaHlbXHVs8PWE7gk3ndkEP8Ft8JOxqjHpO1QAvVenyKDXZ/T5yfd4yLZTfwUD4TMfWlev9f63jSUg==");
          CloudBlobClient clienteBlob = cuentaAlmacenamiento.CreateCloudBlobClient();
          CloudBlobContainer contenedorBlob = clienteBlob.GetContainerReference("lab01");
          CloudBlockBlob recursoBlob = contenedorBlob.GetBlockBlobReference("Foto.jpg");

          var stream = File.OpenWrite(ruta);
          await recursoBlob.DownloadToStreamAsync(stream);
          Android.Net.Uri rutaImagen = Android.Net.Uri.Parse(ruta);
          imagen.SetImageURI(rutaImagen);


          //TableEntity Pendning

          CloudTableClient tableClient = cuentaAlmacenamiento.CreateCloudTableClient();
          CloudTable table = tableClient.GetTableReference("Ubicacion");
          TableOperation retrieveOperation = TableOperation.Retrieve<UbicacionEntity>("Foto.jpg", "México");

          TableResult retrieveResult = await table.ExecuteAsync(retrieveOperation);
          if (retrieveResult.Result != null)
          {
            longitud = ((UbicacionEntity)retrieveResult.Result).Longitud;
            latitud = ((UbicacionEntity)retrieveResult.Result).Latitud;
            mapView = FindViewById<MapView>(Resource.Id.map);
            mapView.OnCreate(bundle);
            mapView.GetMapAsync(this);
            var options = new GoogleMapOptions();
            options.InvokeLiteMode(true);
            MapsInitializer.Initialize(this);
          }
        }
        catch (StorageException ex)
        {
          Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
        }
      };
    }

    public void OnMapReady(GoogleMap googleMap)
    {
      this.googleMap = googleMap;
      CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
      builder.Target(new LatLng(latitud, longitud));
      builder.Zoom(17);
      CameraPosition cameraPosition = builder.Build();
      CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
      this.googleMap.AnimateCamera(cameraUpdate);
    }
  }
}

