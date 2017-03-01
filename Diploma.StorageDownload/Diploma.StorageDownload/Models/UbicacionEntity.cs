using Microsoft.WindowsAzure.Storage.Table;

namespace Diploma.StorageDownload
{
  public class UbicacionEntity: TableEntity
  {
    public UbicacionEntity(string Archivo, string Pais)
    {
      this.PartitionKey = Archivo;
      this.RowKey = Pais;
    }

    public UbicacionEntity() { }

    public double Longitud { get; set; }
    public double Latitud { get; set; }
  }
}