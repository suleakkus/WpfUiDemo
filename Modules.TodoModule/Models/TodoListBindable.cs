using System.Collections.ObjectModel;
using Modules.DatabaseModule;
using Modules.DatabaseModule.Tables;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Modules.TodoModule.Models;

public class TodoListBindable : BindableBase
{
    private readonly TodoContext context;
    private readonly IEventAggregator ea;
    private readonly TodoList list;
    private string title;

    public TodoListBindable(
        IEventAggregator ea,
        TodoList list,
        TodoContext context)
    {
        this.ea = ea;
        this.list = list;
        this.context = context;
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
        var todo = new Todo();
        todo.TodoList = list;
        todo.Name = string.Empty;
        context.Todos.Add(todo);
        
        context.SaveChanges();
        Items.Add(new TodoItemBindable(ea, todo, context));
    }
}