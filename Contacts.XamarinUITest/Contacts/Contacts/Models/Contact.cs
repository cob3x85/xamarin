using System;
using BaseObjects;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace Contacts
{
  [DataTable("Contacts")]
  public class Contact : ObservableBaseObject
  {
    [JsonProperty("Id")]
    public string Id { get; set; }


    [Version]
    public string Version { get; set; }

    private string name;
    [JsonProperty("Name")]
    public string Name
    {
      get { return name; }
      set { name = value; OnPropertyChanged(); }
    }

    private string phoneNumber;
    [JsonProperty("Phone")]
    public string PhoneNumber
    {
      get { return phoneNumber; }
      set { phoneNumber = value; OnPropertyChanged(); }
    }
    public Contact()
    {

    }

    public Contact(string name, string phoneNumber)
    {
      Name = name;
      PhoneNumber = phoneNumber;
    }
  }
}