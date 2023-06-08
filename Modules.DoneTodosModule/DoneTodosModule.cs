using Common;
using MahApps.Metro.IconPacks;
using Modules.DoneTodosModule.Views;
using Prism.Ioc;
using Prism.Regions;

namespace Modules.DoneTodosModule;

public class DoneTodosModule : BaseModule
{

    public DoneTodosModule(
        IRegionManager regionManager,
        IMenuController menuController)
        : base(
            regionManager,
            menuController)
    { }

    public override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        PackIconControlBase icon2 = new PackIconMaterial
        {
            Kind = PackIconMaterialKind.AccountArrowLeft
        };
        MenuController.Add("Tamamlanmış Görevler", RegionName, icon2);
    }

    public override void OnInitialized(IContainerProvider containerProvider)
    {
        RegionManager.RegisterViewWithRegion(RegionName, typeof(DoneTodosView));
    }
}
