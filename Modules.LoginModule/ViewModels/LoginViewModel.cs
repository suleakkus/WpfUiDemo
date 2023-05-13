using System.Linq;
using System.Windows;
using JetBrains.Annotations;
using Modules.DatabaseModule;
using Modules.LoginModule.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace Modules.LoginModule.ViewModels;

[UsedImplicitly]
public class LoginViewModel : BindableBase
{
    private readonly TodoContext context;
    private LoginModel loginModel;

    public LoginViewModel(TodoContext context)
    {
        this.context = context;
        loginModel = new LoginModel();
    }

    public LoginModel LoginModel
    {
        get => loginModel;
        set => SetProperty(ref loginModel, value);
    }

    public DelegateCommand LoginCommand => new(OnLogin);

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
        }

        //TODO buradan TODO ekranına yönlendir.
    }
}