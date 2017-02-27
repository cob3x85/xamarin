using Xamarin.Forms;

namespace Contacts
{
  public partial class LoginPage : ContentPage
  {
    readonly LoginVM context = new LoginVM();

    public LoginPage()
    {
      InitializeComponent();

      this.BindingContext = context;
      context.LoginCompleted += LoginCompleted;
    }

    void LoginCompleted(object sender, LoginEventArgs e)
    {
      if (e.LoginResult == LoginResult.Ok)
        Navigation.PushAsync(new UserProfilePage(context.User));
      else if (e.LoginResult == LoginResult.Error)
        DisplayAlert("Error", "Por favor revise los datos introducidos", "Aceptar");
      else if (e.LoginResult == LoginResult.CommunicationError)
        DisplayAlert("Error", "Ocurrió un error al registrar su actividad, revise su conexión a internet y los datos introducidos", "Aceptar");
    }
  }
}
