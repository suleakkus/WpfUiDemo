using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Modules.TodoModule.Models;

public class TodoListBindable : BindableBase
{
    private readonly IEventAggregator ea;
    private string title;

    public TodoListBindable(IEventAggregator ea)
    {
        this.ea = ea;
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