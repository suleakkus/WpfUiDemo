using System.Linq;
using System.Windows;
using Common;
using Common.Models;
using JetBrains.Annotations;
using Modules.DatabaseModule;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Modules.LoginModule.ViewModels;

[UsedImplicitly]
public class LoginViewModel : BindableBase
{
    private readonly TodoContext context;
    private readonly IEventAggregator ea;
    private LoginModel loginModel;

    public LoginViewModel(TodoContext context, IEventAggregator ea)
    {
        this.context = context;
        this.ea = ea;
        loginModel = new LoginModel();
        ea.GetEvent<Events.NavigateToLoginEvent>().Subscribe(OnNavigatedToLogin);
    }


    public LoginModel LoginModel
    {
        get => loginModel;
        set => SetProperty(ref loginModel, value);
    }

    public DelegateCommand LoginCommand => new(OnLogin);

    public DelegateCommand SignUpCommand => new(OnSignUp);

    private void OnSignUp()
    {
        ea.GetEvent<Events.NavigateToSignUpEvent>().Publish();
    }

    private void OnNavigatedToLogin(LoginModel o)
    {
        LoginModel.Username = o.Username;
        LoginModel.Password = o.Password;
    }

    private void OnLogin()
    {
        if (string.IsNullOrWhiteSpace(LoginModel.Username)
         || string.IsNullOrWhiteSpace(LoginModel.Password))
        {
            MessageBox.Show("Kullanıcı adı ve Şifreyi Boş bırakmayınız");
            return;
        }

        User? user = context.Users.FirstOrDefault(o => o.Username == LoginModel.Username);

        if (user == null)
        {
            MessageBox.Show($"Kullanıcı bulunamadı:{LoginModel.Username}");
            return;
        }

        if (LoginModel.Password != user.Password)
        {
            MessageBox.Show(
                "Kullanıcı adınız veya şifreniz yanlış\nKontrol edip tekrar deneyiniz.",
                "KULLANICI GİRİŞİ",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        ea.GetEvent<Events.LoginEvent>().Publish(LoginModel);
    }
}