using Prism.Mvvm;

namespace Modules.LoginModule.Models;

public class LoginModel : BindableBase
{
    private string? password;
    private string? username;

    public string? Username
    {
        get => username;
        set => SetProperty(ref username, value);
    }

    public string? Password
    {
        get => password;
        set => SetProperty(ref password, value);
    }
}