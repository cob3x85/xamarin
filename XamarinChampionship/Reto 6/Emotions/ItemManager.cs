using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace Emotions
{
  public class ItemManager
  {
    static ItemManager defaultInstance = new ItemManager();
    MobileServiceClient client;

    IMobileServiceTable<TorneoItem> todoTable;

    private ItemManager()
    {
      this.client = new MobileServiceClient(@"https://xamarinchampions.azurewebsites.net/");
      this.todoTable = client.GetTable<TorneoItem>();
    }

    public ItemManager DefaultManager { get; set; }

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
