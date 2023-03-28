using System.Windows;
using MahappsPrism.Desktop.Views;
using Prism.Ioc;

namespace MahappsPrism.Desktop;

public partial class App
{
    protected override void RegisterTypes(IContainerRegistry containerRegistry) { }

    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }
}