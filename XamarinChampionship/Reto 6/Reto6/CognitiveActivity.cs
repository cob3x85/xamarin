using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Emotions;
using System.IO;
using Plugin.Media.Abstractions;

namespace Reto6
{
  [Activity(Label = "Registrar datos")]
  public class CognitiveActivity : Activity
  {

    ItemManager manager;
    static Stream streamCopy;
    string ResultadoEmociones = "Reto6 + MX + Carlos Ortiz Bravo";
    TextView txtResultado;
    Button btnRegistraResultados;
    Button btnAnalizaFoto;

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
      manager = ItemManager.DefaultManager;

      SetContentView(Resource.Layout.Cognitive);

      Button btnCamera = FindViewById<Button>(Resource.Id.btnCamara);
      btnAnalizaFoto = FindViewById<Button>(Resource.Id.btnAnalizarFoto);
      btnRegistraResultados = FindViewById<Button>(Resource.Id.btnRegistraResultados);
      txtResultado = FindViewById<TextView>(Resource.Id.txtOutput);

      btnRegistraResultados.Visibility = ViewStates.Invisible;
      btnAnalizaFoto.Visibility = ViewStates.Invisible;
      btnCamera.Click += BtnCamera_Click;
      btnAnalizaFoto.Click += BtnAnalizaFoto_Click;
      btnRegistraResultados.Click += BtnRegistraResultados_Click;

    }

    private void BtnRegistraResultados_Click(object sender, EventArgs e)
    {
      btnRegistraResultados.Visibility = ViewStates.Invisible;
      Toast.MakeText(this, "Registrando tus resultados", ToastLength.Short).Show();
      TorneoItem registro = new TorneoItem();
      registro.DeviceId = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);
      registro.Email = "cob3x85@outlook.com";
      registro.Reto = ResultadoEmociones;
    }

    private void BtnAnalizaFoto_Click(object sender, EventArgs e)
    {
      throw new NotImplementedException();
    }

    private async void BtnCamera_Click(object sender, EventArgs e)
    {
      MediaFile file = null;
      try
      {
        file = await ImageService.TakePicture(true);
      }
      catch (Android.OS.OperationCanceledException) { }
      SetImageToControl(file);
      btnAnalizaFoto.Visibility = ViewStates.Visible;
    }
    private void SetImageToControl(MediaFile file)
    {
      if (file == null)
      {
        return;
      }
      ImageView imgImage = FindViewById<ImageView>(Resource.Id.imageViewFoto);
      imgImage.SetImageURI(Android.Net.Uri.Parse(file.Path));
      var stream = file.GetStream();
      streamCopy = new MemoryStream();
      stream.CopyTo(streamCopy);
      stream.Seek(0, SeekOrigin.Begin);
      file.Dispose();
    }

  }
}