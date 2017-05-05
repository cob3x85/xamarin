using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace Emotions
{
  public partial class ItemManager
  {
    static ItemManager defaultInstance = new ItemManager();
    MobileServiceClient client;

    IMobileServiceTable<TorneoItem> todoTable;

    private ItemManager()
    {
      this.client = new MobileServiceClient(@"https://xamarinchampions.azurewebsites.net/");
      this.todoTable = client.GetTable<TorneoItem>();
    }

    public static ItemManager DefaultManager
    {
      get
      {
        return defaultInstance;
      }

      private set => defaultInstance = value;
    }

    public MobileServiceClient CurrentClient
    {
      get { return client; }
    }

    public async Task SaveTaskAsync(TorneoItem item)
    {
      if(item.Id == null)
      {
        await todoTable.InsertAsync(item);
      }
      else
      {
        await todoTable.UpdateAsync(item);
      }
    }
  }
}
