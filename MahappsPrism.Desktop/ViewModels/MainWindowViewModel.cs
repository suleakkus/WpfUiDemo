using System.Collections.ObjectModel;
using Common;
using Common.Models;
using JetBrains.Annotations;
using MahApps.Metro.Controls;
using Prism.Events;
using Prism.Mvvm;

namespace MahappsPrism.Desktop.ViewModels;

[UsedImplicitly]
public class MainWindowViewModel : BindableBase
{
    private readonly IMenuController menuController;

    private bool isFlyoutOpen;

    private LoginModel loginModel;
    private string title;

    public MainWindowViewModel(
        IMenuController menuController,
        IEventAggregator ea)
    {
        this.menuController = menuController;
        title = "My Title";
        Items = new ObservableCollection<HamburgerMenuIconItem>();
        ea.GetEvent<Events.LoginEvent>().Subscribe(OnLogin);
        isFlyoutOpen = true;
    }

    public string Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }

    public ObservableCollection<HamburgerMenuIconItem> Items { get; set; }

    public bool IsFlyoutOpen
    {
        get => isFlyoutOpen;
        set => SetProperty(ref isFlyoutOpen, value);
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
    }

    private void OnLogin(LoginModel o)
    {
        LoginModel = o;
        IsFlyoutOpen = false;
    }
}