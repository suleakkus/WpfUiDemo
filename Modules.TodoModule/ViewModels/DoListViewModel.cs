using System.Collections.ObjectModel;
using System.Collections.Specialized;
using JetBrains.Annotations;
using Modules.TodoModule.Events;
using Modules.TodoModule.Models;
using Prism.Events;
using Prism.Mvvm;

namespace Modules.TodoModule.ViewModels;

[UsedImplicitly]
public class DoListViewModel : BindableBase
{
    private readonly TodoList dones;
    private readonly IEventAggregator ea;
    private readonly TodoList todos;

    public DoListViewModel(IEventAggregator ea)
    {
        this.ea = ea;
        dones = new TodoList(ea);
        todos = new TodoList(ea);

        TodoLists = new ObservableCollection<TodoList>();

        todos.Title = "Todo's";
        todos.Items.Add(
            new TodoItem(ea)
            {
                Text = "Todo 1"
            });
        todos.Items.Add(
            new TodoItem(ea)
            {
                Text = "Todo 2"
            });
        todos.Items.Add(
            new TodoItem(ea)
            {
                Text = "Todo 3"
            });

        dones.Title = "Done";
        dones.Items.Add(
            new TodoItem(ea)
            {
                Text = "Done 1",
                IsDone = true
            });
        dones.Items.Add(
            new TodoItem(ea)
            {
                Text = "Done 2",
                IsDone = true
            });
        dones.Items.Add(
            new TodoItem(ea)
            {
                Text = "Done 3",
                IsDone = true
            });

        TodoLists.Add(todos);
        TodoLists.Add(dones);

        ea.GetEvent<TodoEvents.TodoItemFinishedEvent>().Subscribe(OnTodoFinish);
        ea.GetEvent<TodoEvents.TodoItemUnFinishedEvent>().Subscribe(OnTodoUnFinish);
        ea.GetEvent<TodoEvents.TodoItemDeleteEvent>().Subscribe(ToDoDelete);

        dones.Items.CollectionChanged += ItemsOnCollectionChanged;
    }

    public ObservableCollection<TodoList> TodoLists { get; }

    public void CreateSection(string sectionName)
    {
        var todoList = new TodoList(ea);
        todoList.Title = sectionName;
        TodoLists.Add(todoList);
    }

    private void ItemsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                if (e.NewItems == null)
                {
                    break;
                }

                foreach (TodoItem item in e.NewItems)
                {
                    item.IsDone = true;
                }

                break;
            case NotifyCollectionChangedAction.Remove:

                if (e.OldItems == null)
                {
                    break;
                }

                foreach (TodoItem item in e.OldItems)
                {
                    item.IsDone = false;
                }

                break;
        }
    }

    private void ToDoDelete(TodoItem item)
    {
        foreach (TodoList todoList in TodoLists)
        {
            for (var i = 0; i < todoList.Items.Count; i++)
            {
                TodoItem? todo = todoList.Items[i];
                if (todo == item)
                {
                    todoList.Items.Remove(item);
                    return;
                }
            }
        }
    }

    private void OnTodoFinish(TodoItem item)
    {
        todos.Items.Remove(item);
        dones.Items.Add(item);
    }

    private void OnTodoUnFinish(TodoItem item)
    {
        dones.Items.Remove(item);
        todos.Items.Add(item);
    }
}