using System.Linq;
using System.Windows;
using JetBrains.Annotations;
using Modules.DatabaseModule;
using Prism.Commands;
using Prism.Mvvm;

namespace Modules.LoginModule.ViewModels;

[UsedImplicitly]
public class SignUpViewModel : BindableBase
{
    private readonly TodoContext context;
    private string name;
    private string password;
    private string surname;
    private string username;

    public SignUpViewModel(TodoContext context)
    {
        this.context = context;
        name = string.Empty;
        password = string.Empty;
        surname = string.Empty;
        username = string.Empty;
    }

    public string Username
    {
        get => username;
        set => SetProperty(ref username, value);
    }

    public string Password
    {
        get => password;
        set => SetProperty(ref password, value);
    }

    public string Name
    {
        get => name;
        set => SetProperty(ref name, value);
    }

    public string Surname
    {
        get => surname;
        set => SetProperty(ref surname, value);
    }

    public DelegateCommand SignUpCommand => new(OnSignUp);

    private void OnSignUp()
    {
        if (string.IsNullOrWhiteSpace(Username)
         || string.IsNullOrWhiteSpace(Password)
         || string.IsNullOrWhiteSpace(Name)
         || string.IsNullOrWhiteSpace(Surname))
        {
            MessageBox.Show("Bilgilerinizi Boş Bırakmayınız");
            return;
        }

        bool isAlreadyRegistered = context.Users.Any(o => o.Username == Username);
        if (isAlreadyRegistered)
        {
            MessageBox.Show($"{Username} kullanılıyor. Başka Username giriniz.");
            return;
        }

        var entity = new User();
        entity.Name = Name;
        entity.Password = Password;
        entity.Surname = Surname;
        entity.Username = Username;

        context.Users.Add(entity);
        context.SaveChanges();
    }
}