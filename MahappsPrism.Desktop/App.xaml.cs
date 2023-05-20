using System.Windows;
using Common;
using MahappsPrism.Desktop.Controllers;
using MahappsPrism.Desktop.Views;
using Modules.DatabaseModule;
using Modules.LoginModule;
using Modules.TodoModule;
using Prism.Ioc;
using Prism.Modularity;

namespace MahappsPrism.Desktop;

public partial class App
{
    protected override void RegisterTypes(IContainerRegistry registry)
    {
        var todoContext = new TodoContext();
        todoContext.Database.EnsureCreated();
        registry.RegisterInstance(todoContext);

        registry.RegisterSingleton<IMenuController, MenuController>();
    }


    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        moduleCatalog.AddModule<TodoModule>();
        moduleCatalog.AddModule<LoginModule>();
    }

    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }
}