using System;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Common;

public abstract class BaseModule : IModule
{
    protected readonly IMenuController MenuController;
    protected readonly IRegionManager RegionManager;

    public BaseModule(
        IRegionManager regionManager,
        IMenuController menuController)
    {
        RegionManager = regionManager;
        MenuController = menuController;
    }

    protected string RegionName { get; } = Guid.NewGuid().ToString();

    public abstract void RegisterTypes(IContainerRegistry containerRegistry);

    public abstract void OnInitialized(IContainerProvider containerProvider);
}