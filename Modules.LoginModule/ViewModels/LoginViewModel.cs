using System.Windows;
using JetBrains.Annotations;
using Modules.LoginModule.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace Modules.LoginModule.ViewModels;

[UsedImplicitly]
public class LoginViewModel : BindableBase
{
    private LoginModel loginModel;

    public LoginViewModel()
    {
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
        MessageBox.Show($"{LoginModel.Username} {LoginModel.Password}");
    }
}