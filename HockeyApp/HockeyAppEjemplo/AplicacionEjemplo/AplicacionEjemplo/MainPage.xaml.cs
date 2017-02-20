using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AplicacionEjemplo
{
  public partial class MainPage : ContentPage
  {
    public MainPage()
    {
      InitializeComponent();
      this.btnError.Clicked += (sender, obj) =>
      {
        throw new Exception("Error manual!");
      };

      this.btnEvento1.Clicked += (sender, obj) =>
      {
        HockeyApp.MetricsManager.TrackEvent("Evento 1");
      };

    }

  }
}
