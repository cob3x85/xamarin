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
using Microsoft.WindowsAzure.Storage.Table;

namespace Diploma.AzureStorage.Model
{
  public class UbicacionEntity: TableEntity
  {
    public UbicacionEntity(string Archivo, string Pais)
    {
      this.PartitionKey = Archivo;
      this.RowKey = Pais;
    }

    public UbicacionEntity() { }

    public string Latitud { get; set; }
    public string Longitud { get; set; }
    public string Localidad { get; set; }
  }
}