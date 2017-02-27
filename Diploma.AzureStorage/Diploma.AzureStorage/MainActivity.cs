using Android.App;
using Android.Widget;
using Android.OS;
using XamarinDiplomado.Participants.Startup;
using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using Diploma.AzureStorage.Model;
using System.Net;
using System.IO;

namespace Diploma.AzureStorage
{
  [Activity(Label = "Diploma.AzureStorage", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {

    ImageView ImagenDrop;
    string archivoLocal;

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);
      Button btnImagen = FindViewById<Button>(Resource.Id.btnRealizar);
      ImagenDrop = FindViewById<ImageView>(Resource.Id.imagen);

      btnImagen.Click += ArchivoImagen;

      Startup startup = new Startup("Carlos Ortiz Bravo", "cob3x85@outlook.com", 1, 1);
      startup.Init();
    }

    async void ArchivoImagen(object sender, System.EventArgs e)
    {
      var ruta = await DescargarImagen();
      Android.Net.Uri rutaImagen = Android.Net.Uri.Parse(ruta);
      ImagenDrop.SetImageURI(rutaImagen);

      //Agregamos la conexión a CloudStorage

      CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=cobxamarin;AccountKey=wWWEUzzSVVaHlbXHVs8PWE7gk3ndkEP8Ft8JOxqjHpO1QAvVenyKDXZ/T5yfd4yLZTfwUD4TMfWlev9f63jSUg==");
      CloudBlobClient clientBlob = storageAccount.CreateCloudBlobClient();
      CloudBlobContainer containerBlob = clientBlob.GetContainerReference("lab01");
      CloudBlockBlob recursoBlob = containerBlob.GetBlockBlobReference(archivoLocal);
      await recursoBlob.UploadFromFileAsync(ruta);

      //Agregamos un mensaje de guardado
      Toast.MakeText(this, "Guardado en AzureStorage Blob", ToastLength.Long).Show();


      //Agregamos código para la tabla NoSQL
      CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
      CloudTable table = tableClient.GetTableReference("Ubicaciones");

      await table.CreateIfNotExistsAsync();
      UbicacionEntity ubica = new UbicacionEntity(archivoLocal, "México");
      ubica.Latitud = "20.677812";
      ubica.Localidad = "Guadalajara";
      ubica.Longitud = "-103.379642";

      TableOperation insertar = TableOperation.Insert(ubica);
      await table.ExecuteAsync(insertar);

      Toast.MakeText(this, "Guardando en Azure Storage Table NoSQL", ToastLength.Long).Show();

    }

    public async Task<string> DescargarImagen()
    {
      WebClient client = new WebClient();
      byte[] imageData = await client.DownloadDataTaskAsync("https://dl.dropboxusercontent.com/u/95408124/foto1.jpg");
      string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
      archivoLocal = "foto1.jpg";
      string localPath = Path.Combine(documentsPath, archivoLocal);
      File.WriteAllBytes(localPath, imageData);
      return localPath;
    }
  }
}

