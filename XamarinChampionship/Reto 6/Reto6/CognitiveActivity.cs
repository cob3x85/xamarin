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

namespace Reto6
{
  [Activity(Label = "Registrar datos")]
  public class CognitiveActivity : Activity
  {

    ItemManager manager;
    static Stream streamCopy;
    string ResultEmotions = "Reto6 + MX + Carlos Ortiz Bravo";
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
      //btnRegistraResultados
    }

  }
}