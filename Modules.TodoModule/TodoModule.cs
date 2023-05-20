using Common;
using MahApps.Metro.IconPacks;
using Modules.TodoModule.Views;
using Prism.Ioc;
using Prism.Regions;

namespace Modules.TodoModule;

public class TodoModule : BaseModule
{
    
    public TodoModule(
        IRegionManager regionManager,
        IMenuController menuController)
        : base(
            regionManager,
            menuController) { }

    public override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        PackIconControlBase icon = new PackIconMaterial
        {
            Kind = PackIconMaterialKind.NoteTextOutline
        };
        MenuController.Add("Yapılacaklar", RegionName, icon);
    }

    public override void OnInitialized(IContainerProvider containerProvider)
    {
        RegionManager.RegisterViewWithRegion(RegionName, typeof(DoListView));
    }
}