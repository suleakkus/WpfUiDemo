using System.Windows;
using MahappsPrism.Desktop.Views;
using Modules.DatabaseModule;
using Prism.Ioc;

namespace MahappsPrism.Desktop;

public partial class App
{
    protected override void RegisterTypes(IContainerRegistry registry)
    {
        registry.RegisterSingleton<TodoContext>();
    }

    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }
}