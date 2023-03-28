using System.Windows;
using Prism.Ioc;
using WpfUiDemo.Desktop.Views;

namespace WpfUiDemo.Desktop;

public partial class App
{
    /// <inheritdoc />
    protected override void RegisterTypes(IContainerRegistry containerRegistry) { }

    /// <inheritdoc />
    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }
}