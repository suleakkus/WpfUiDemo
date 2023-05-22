using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Common;
using Common.Models;
using JetBrains.Annotations;
using MahApps.Metro.Controls;
using Modules.DatabaseModule;
using Modules.DatabaseModule.Tables;
using Prism.Events;
using Prism.Mvvm;

namespace MahappsPrism.Desktop.ViewModels;

[UsedImplicitly]
public class MainWindowViewModel : BindableBase
{
    private readonly IMenuController menuController;
    private readonly TodoContext context;
    private bool isLoginFlyoutOpen;
    private bool isSignUpFlyoutOpen;
    private LoginModel loginModel;
    private string title;

    public MainWindowViewModel(
        IMenuController menuController,
        IEventAggregator ea,
        TodoContext context)
    {
        this.menuController = menuController;
        this.context = context;
        title = "My Title";
        Items = new ObservableCollection<HamburgerMenuIconItem>();
        isLoginFlyoutOpen = true;
        loginModel = new LoginModel();
        ea.GetEvent<Events.LoginEvent>().Subscribe(OnLogin);
        ea.GetEvent<Events.NavigateToSignUpEvent>().Subscribe(NavigateToSignUp);
        ea.GetEvent<Events.NavigateToLoginEvent>().Subscribe(NavigateToLogin);
    }

    public string Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }

    public ObservableCollection<HamburgerMenuIconItem> Items { get; set; }

    public bool IsLoginFlyoutOpen
    {
        get => isLoginFlyoutOpen;
        set => SetProperty(ref isLoginFlyoutOpen, value);
    }

    public bool IsSignUpFlyoutOpen
    {
        get => isSignUpFlyoutOpen;
        set => SetProperty(ref isSignUpFlyoutOpen, value);
    }

    public LoginModel LoginModel
    {
        get => loginModel;
        set => SetProperty(ref loginModel, value);
    }

    public void Start()
    {
        foreach (HamburgerMenuIconItem item in menuController.Items)
        {
            Items.Add(item);
        }
        
        //Kullanıcıları bir kereliğine çekiyoruz ki database bağlantısını oluşturup
        //kayıt ol ekranında bekleme yapmasın
        //uygulamayı aslında daha hızlı yapmıyor
        //sadece kayıt ol ekranında beklemek yerine burada bekliyor ilk seferinde
        List<User> users = context.Users.ToList();
    }

    private void OnLogin(LoginModel o)
    {
        LoginModel = o;
        IsLoginFlyoutOpen = false;
    }

    private void NavigateToLogin(LoginModel o)
    {
        IsLoginFlyoutOpen = true;
        IsSignUpFlyoutOpen = false;
    }

    private void NavigateToSignUp()
    {
        IsLoginFlyoutOpen = false;
        IsSignUpFlyoutOpen = true;
    }
}