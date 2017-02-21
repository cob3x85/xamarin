using BaseObjects;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts
{
  public class LoginVM : ObservableBaseObject
  {
    public ICommand LoginCommand { get; set; }

    public bool IsValidEmail
    {
      get;
      set;
    }

    private bool isBusy;
    public bool IsBusy
    {
      get { return isBusy; }
      set { isBusy = value; OnPropertyChanged(); }
    }

    private string email;
    public string EMail
    {
      get { return email; }
      set { email = value; OnPropertyChanged(); }
    }

    private string userName;
    public String UserName
    {
      get { return userName; }
      set { userName = value; OnPropertyChanged(); }
    }

    public User User
    {
      get;
      private set;
    }


    private string password;
    public String Password
    {
      get { return password; }
      set { password = value; OnPropertyChanged(); }
    }

    public event EventHandler<LoginEventArgs> LoginCompleted;

    protected virtual void OnLoginCompleted(LoginEventArgs e)
    {
      LoginCompleted?.Invoke(this, e);
    }

    public LoginVM()
    {
      LoginCommand = new Command(async () => await Login());
    }

    private async Task Login()
    {
      if (!IsBusy)
      {
        IsBusy = true;
        User = new User(UserName, Password);
        //if (!IsValidEmail)
        //  OnLoginCompleted(new LoginEventArgs(LoginResult.Error));

        switch (await User.Login())
        {
          case LoginResult.Ok:
            OnLoginCompleted(new LoginEventArgs(LoginResult.Ok));
            break;
          case LoginResult.CommunicationError:
            OnLoginCompleted(new LoginEventArgs(LoginResult.Error));
            break;
        }
        IsBusy = false;
      }
    }
  }
}