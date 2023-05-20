using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Common.Models;
using JetBrains.Annotations;
using Modules.DatabaseModule;
using Modules.DatabaseModule.Tables;
using Modules.TodoModule.Events;
using Modules.TodoModule.Models;
using Prism.Events;
using Prism.Mvvm;

namespace Modules.TodoModule.ViewModels;

[UsedImplicitly]
public class DoListViewModel : BindableBase
{
    private readonly TodoContext context;
    private readonly IEventAggregator ea;
    private LoginModel? loginUser;

    public DoListViewModel(IEventAggregator ea, TodoContext context)
    {
        this.ea = ea;
        this.context = context;
        // ea.GetEvent<TodoEvents.TodoItemFinishedEvent>().Subscribe(OnTodoFinish);
        // ea.GetEvent<TodoEvents.TodoItemUnFinishedEvent>().Subscribe(OnTodoUnFinish);
        ea.GetEvent<TodoEvents.TodoItemDeleteEvent>().Subscribe(OnToDoDelete);

        ea.GetEvent<Common.Events.LoginEvent>().Subscribe(OnUserLogin);

        //dones.Items.CollectionChanged += ItemsOnCollectionChanged;
    }

    public ObservableCollection<TodoListBindable> TodoLists { get; } = new();

    public void CreateSection(string sectionName)
    {
        var todoList = new TodoListBindable(ea);
        todoList.Title = sectionName;
        TodoLists.Add(todoList);
    }

    private void OnToDoDelete(TodoItemBindable value)
    {
        Todo todo = context.Todos.First(o => o.Equals(value.Entity));

        context.Todos.Remove(todo);
        foreach (TodoListBindable list in TodoLists)
        {
            list.Items.Remove(value);
        }

        context.SaveChanges();
    }

    private void OnUserLogin(LoginModel model)
    {
        loginUser = model;
        TodoLists.Clear();
        foreach (TodoList list in context.Lists.Where(o => o.User.Username == loginUser.Username))
        {
            var todoList = new TodoListBindable(ea)
            {
                Title = list.Name
            };

            foreach (Todo todo in context.Todos.Where(o => o.TodoList.TodoListId == list.TodoListId))
            {
                todoList.Items.Add(
                    new TodoItemBindable(ea, todo)
                    {
                        Text = todo.Name,
                        IsDone = todo.IsDone
                    });
            }

            TodoLists.Add(todoList);
        }
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

                foreach (TodoItemBindable item in e.NewItems)
                {
                    item.IsDone = true;
                }

                break;
            case NotifyCollectionChangedAction.Remove:

                if (e.OldItems == null)
                {
                    break;
                }

                foreach (TodoItemBindable item in e.OldItems)
                {
                    item.IsDone = false;
                }

                break;
            case NotifyCollectionChangedAction.Replace:
                break;
            case NotifyCollectionChangedAction.Move:
                break;
            case NotifyCollectionChangedAction.Reset:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}