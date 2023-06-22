using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Common;
using Common.Models;
using ControlzEx.Theming;
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
    private readonly TodoContext context;
    private readonly IMenuController menuController;
    private bool isLoginFlyoutOpen;
    private bool isSignUpFlyoutOpen;
    private LoginModel loginModel;
    private Theme? selectedTheme;
    private string title;

    public MainWindowViewModel(
        IMenuController menuController,
        IEventAggregator ea,
        TodoContext context)
    {
        Themes = new ObservableCollection<Theme>();

        void SetupDarkThemes()
        {
            ReadOnlyObservableCollection<Theme> themes = ThemeManager.Current.Themes;
            IEnumerable<Theme> list = themes.Where(o => o.Name.StartsWith("Dark."));
            Themes.AddRange(list);
            Theme? theme = ThemeManager.Current.DetectTheme(Application.Current);
            selectedTheme = theme;
        }

        this.menuController = menuController;
        this.context = context;
        title = "TODO Uygulaması";
        Items = new ObservableCollection<HamburgerMenuIconItem>();
        isLoginFlyoutOpen = true;
        loginModel = new LoginModel();
        ea.GetEvent<Events.LoginEvent>().Subscribe(OnLogin);
        ea.GetEvent<Events.NavigateToSignUpEvent>().Subscribe(NavigateToSignUp);
        ea.GetEvent<Events.NavigateToLoginEvent>().Subscribe(NavigateToLogin);

        SetupDarkThemes();
    }

    public ObservableCollection<Theme> Themes { get; }

    public Theme? SelectedTheme
    {
        get => selectedTheme;
        set => SetProperty(ref selectedTheme, value, ChangeTheme);
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

    private void ChangeTheme()
    {
        if (SelectedTheme == null)
        {
            return;
        }

        ThemeManager.Current.ChangeTheme(Application.Current, SelectedTheme);
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