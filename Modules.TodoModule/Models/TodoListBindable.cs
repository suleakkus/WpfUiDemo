using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    private string title;

    public TodoListBindable(
        IEventAggregator ea,
        TodoList list,
        TodoContext context,
        List<TodoItemBindable> items)
    {
        this.ea = ea;
        this.TodoList = list;
        this.context = context;
        title = string.Empty;
        items.Sort(TodoItemBindable.OrderComparison);
        Items = new ObservableCollection<TodoItemBindable>(items);
        Items.CollectionChanged += ItemsOnCollectionChanged;
    }

    public TodoListBindable(
        IEventAggregator ea,
        TodoList list,
        TodoContext context)
    {
        this.ea = ea;
        TodoList = list;
        this.context = context;
        title = string.Empty;
        Items = new ObservableCollection<TodoItemBindable>();
        Items.CollectionChanged += ItemsOnCollectionChanged;
    }

    public string Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }

    public ObservableCollection<TodoItemBindable> Items { get; }

    public DelegateCommand AddItemCommand => new(OnAddItem);

    public TodoList TodoList { get; }


    private void ItemsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            if (e.NewItems == null)
            {
                return;
            }

            foreach (TodoItemBindable item in e.NewItems)
            {
                item.Todo.TodoList = TodoList;
                context.Todos.Update(item.Todo);
            }

            for (var i = 0; i < Items.Count; i++)
            {
                TodoItemBindable item = Items[i];
                item.Todo.Order = i;
            }

            context.SaveChanges();
        }
        else if (e.Action is NotifyCollectionChangedAction.Remove or NotifyCollectionChangedAction.Move)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                TodoItemBindable item = Items[i];
                item.Todo.Order = i;
            }

            context.SaveChanges();
        }
    }

    private void OnAddItem()
    {
        var todo = new Todo();
        todo.TodoList = TodoList;
        todo.Order = Items.Count;
        context.Todos.Add(todo);

        context.SaveChanges();
        Items.CollectionChanged -= ItemsOnCollectionChanged;
        Items.Add(new TodoItemBindable(ea, todo, context));
        Items.CollectionChanged += ItemsOnCollectionChanged;
    }
}