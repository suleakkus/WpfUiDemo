using System.Collections.ObjectModel;
using JetBrains.Annotations;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using MahappsPrism.Desktop.Views;
using Modules.LoginModule.Views;
using Modules.TodoModule;
using Modules.TodoModule.Views;
using Prism.Mvvm;

namespace MahappsPrism.Desktop.ViewModels;

[UsedImplicitly]
public class MainWindowViewModel : BindableBase
{
    private string title;

    public MainWindowViewModel()
    {
        title = "My Title";
        Items = new ObservableCollection<HamburgerMenuIconItem>();

        PackIconControlBase icon = new PackIconMaterial
        {
            Kind = PackIconMaterialKind.NoteTextOutline
        };

        var firstView = new LoginView();
        Items.Add(CreateMenu("Giriş Yap", firstView, icon));
        Items.Add(CreateMenu("Kayıt Ol", new UserControl1(), icon));
        Items.Add(CreateMenu("Yapılacaklar", new DoListView(), icon));
        Items.Add(CreateMenu("Menu IV", new LoginView(), icon));
        var secondView = new LoginView();
        Items.Add(CreateMenu("Menu X", secondView, icon));
    }

    public string Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }

    public ObservableCollection<HamburgerMenuIconItem> Items { get; set; }

    private static HamburgerMenuIconItem CreateMenu(string label, object view, PackIconControlBase icon)
    {
        var menuOne = new HamburgerMenuIconItem();
        menuOne.Icon = icon;
        menuOne.Label = label;
        menuOne.Tag = view;
        return menuOne;
    }
}