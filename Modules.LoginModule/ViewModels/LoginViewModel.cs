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
        MessageBox.Show($"{LoginModel.Username} {LoginModel.Password}");
    }
}