using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Modules.TodoModule.Models;

public class TodoList : BindableBase
{
    private readonly IEventAggregator ea;
    private string title;

    public TodoList(IEventAggregator ea)
    {
        this.ea = ea;
        title = string.Empty;
        Items = new ObservableCollection<TodoItem>();
    }

    public string Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }

    public ObservableCollection<TodoItem> Items { get; }

    public DelegateCommand AddItemCommand => new(OnAddItem);

    private void OnAddItem()
    {
        Items.Add(new TodoItem(ea));
    }
}