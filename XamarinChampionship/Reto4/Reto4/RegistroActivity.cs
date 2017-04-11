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
using Reto4.Services;

namespace Reto4
{
  [Activity(Label = "Registrar datos")]
  public class RegistroActivity : Activity
  {

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      SetContentView(Resource.Layout.Registro);
      FindViewById<Button>(Resource.Id.btnConsulta).Click += OnBtnConsultaClick;
      FindViewById<TextView>(Resource.Id.editTextEmail).Text = "cob3x85@outlook.com";
    }

    async void OnBtnConsultaClick(object sender, EventArgs e)
    {
      try
      {
        ServiceHelper serviceHelper = new ServiceHelper();
        // Retrieve the values the user entered into the UI
        string AndroidId = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);
        string Email = FindViewById<TextView>(Resource.Id.editTextEmail).Text;
        string reto = Intent.GetStringExtra("Reto");
        var items = await serviceHelper.BuscarRegistros(Email);
        var registros = FindViewById<TextView>(Resource.Id.txtRegistros);
        var builderData = new StringBuilder();
        var totalRegistros = items.Count;
        var newReto = string.Empty;
        if (totalRegistros > 0)
        {
          foreach (var item in items)
          {
            var cadena = item.Reto.ToLowerInvariant().Equals("reto4") ? $"{item.Id} {item.Email} {item.Reto}+{totalRegistros}" : $"{item.Id} {item.Email} {item.Reto}";
            newReto = item.Reto.ToLowerInvariant().Equals("reto4") && newReto.Length == 0 ? $"{item.Reto}+{totalRegistros}" : newReto;
            builderData.AppendLine(cadena);
          }
        }
        registros.Text = builderData.ToString();
        //await serviceHelper.InsertarEntidad(Email, newReto, AndroidId);
        SetResult(Result.Ok, Intent);

      }
      catch (Exception exc)
      {
        Toast.MakeText(this, exc.Message, ToastLength.Long).Show();
        SetResult(Result.Canceled, Intent);
      }
      Finish();
    }
  }
}