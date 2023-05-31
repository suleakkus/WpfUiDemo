using System.Collections.ObjectModel;
using Modules.DatabaseModule.Tables;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Modules.TodoModule.Models;

public class TodoListBindable : BindableBase
{
    private readonly IEventAggregator ea;
    private readonly TodoList list;
    private string title;

    public TodoListBindable(IEventAggregator ea, TodoList list)
    {
        this.ea = ea;
        this.list = list;
        title = string.Empty;
        Items = new ObservableCollection<TodoItemBindable>();
    }

    public string Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }

    public ObservableCollection<TodoItemBindable> Items { get; }

    public DelegateCommand AddItemCommand => new(OnAddItem);

    private void OnAddItem()
    {
        //TODO 
        //Items.Add(new TodoItem(ea));
    }
}