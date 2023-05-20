using System;
using System.Linq;
using System.Windows;
using Common;
using Common.Models;
using JetBrains.Annotations;
using Modules.DatabaseModule;
using Modules.DatabaseModule.Tables;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Modules.LoginModule.ViewModels;

[UsedImplicitly]
public class SignUpViewModel : BindableBase
{
    private readonly TodoContext context;
    private readonly IEventAggregator ea;
    private string name;
    private string password;
    private string surname;
    private string username;

    public SignUpViewModel(TodoContext context, IEventAggregator ea)
    {
        this.context = context;
        this.ea = ea;
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

        try
        {
            SaveUserToDatabase();
            NavigateToLogin();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.ToString(), exception.Message, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void NavigateToLogin()
    {
        var loginModel = new LoginModel
        {
            Username = username,
            Password = password
        };

        ea.GetEvent<Events.NavigateToLoginEvent>().Publish(loginModel);
    }

    private void SaveUserToDatabase()
    {
        var entity = new User();
        entity.Name = Name;
        entity.Password = Password;
        entity.Surname = Surname;
        entity.Username = Username;

        context.Users.Add(entity);
        context.SaveChanges();
    }
}