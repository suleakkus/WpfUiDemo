using System.Collections.ObjectModel;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;

namespace Common;

public interface IMenuController
{
    ObservableCollection<HamburgerMenuIconItem> Items { get; }
    void Add(string label, string regionName, PackIconControlBase icon);
}