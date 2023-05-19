using Common;
using Modules.LoginModule.Views;
using Prism.Ioc;
using Prism.Regions;

namespace Modules.LoginModule;

public class LoginModule : BaseModule
{
    public LoginModule(IRegionManager regionManager, IMenuController menuController) : base(
        regionManager,
        menuController) { }

    public static string LoginRegionName => "LoginRegion";

    public override void RegisterTypes(IContainerRegistry containerRegistry)
    {
    }

    public override void OnInitialized(IContainerProvider containerProvider)
    {
        RegionManager.RegisterViewWithRegion<LoginView>(LoginRegionName);
    }
}