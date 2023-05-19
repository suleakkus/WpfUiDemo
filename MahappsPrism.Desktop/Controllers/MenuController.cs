using System.Collections.ObjectModel;
using System.Windows.Controls;
using Common;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using Prism.Regions;

namespace MahappsPrism.Desktop.Controllers;

public class MenuController : IMenuController
{
    public MenuController()
    {
        Items = new ObservableCollection<HamburgerMenuIconItem>();
    }

    public ObservableCollection<HamburgerMenuIconItem> Items { get; set; }

    public void Add(string label, string regionName, PackIconControlBase icon)
    {
        var contentControl = new ContentControl();
        var item = new HamburgerMenuIconItem
        {
            Label = label,
            Tag = contentControl,
            Icon = icon
        };
        
        RegionManager.SetRegionName(contentControl, regionName);
        
        Items.Add(item);
    }
}