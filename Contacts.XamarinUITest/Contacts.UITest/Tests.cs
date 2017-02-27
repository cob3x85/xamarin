using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Contacts.UITest
{
  [TestFixture(Platform.Android)]
  [TestFixture(Platform.iOS)]
  public class Tests
  {
    IApp app;
    Platform platform;

    public Tests(Platform platform)
    {
      this.platform = platform;
    }

    [SetUp]
    public void BeforeEachTest()
    {
      app = AppInitializer.StartApp(platform);
    }

    [Test]
    public void AppLaunches()
    {
      app.Screenshot("Launched");
      app.Repl();
    }

    [Test]
    public void LoginWillSuccess()
    {
      app.EnterText("entUsername", "user1");
      app.EnterText("entPassword", "asdfg");
      app.Tap("btnLogin");
      app.WaitForNoElement("indIsBusy", "No desaparecio IsBusy", new TimeSpan(0, 0, 10), null, new TimeSpan(0, 0, 5));
      var perfil = app.Query("Perfil").First();
      Assert.NotNull(perfil, "No se pudo logear correctamente");
    }

    [Test]
    public void LoginWillFail()
    {
      app.EnterText("entUsername", "asdfg");
      //app.EnterText("entPassword", "asdfg");
      app.Tap("btnLogin");
      app.WaitForNoElement("indIsBusy", "No desaparecio IsBusy", new TimeSpan(0, 0, 10), null, new TimeSpan(0, 0, 5));
      var errorDialog = app.Query("Error");
      Assert.IsTrue(errorDialog.Any(), "No apareció el mensaje de error");
    }

    [Test]
    public void CellWillBePressed()
    {

      LoginWillSuccess();

      app.Tap("entBirthday");
      app.EnterText("entBirthday", "03/10/1985");
      app.Tap("btnSaveProfile");
      app.WaitForNoElement("indIsBusy", "No desaparecio IsBusy", new TimeSpan(0, 0, 10), null, new TimeSpan(0, 0, 5));
      if (platform == Platform.Android)
      {
        app.TouchAndHold(x => x.Index(0));
      }
      else
      {
        app.SwipeRightToLeft(x => x.Index(0));
      }
      var deleteOptions = app.Query("Eliminar");
      app.Screenshot("Se ha presionado la celda");
      Assert.GreaterOrEqual(deleteOptions.Count(), 1, "No se selecciono una celda");
    }
  }
}

