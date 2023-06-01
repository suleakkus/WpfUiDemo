using System.Collections.Generic;
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

    public DoListViewModel(
        IEventAggregator ea,
        TodoContext context)
    {
        this.ea = ea;
        this.context = context;
        ea.GetEvent<TodoEvents.TodoItemDeleteEvent>().Subscribe(OnToDoDelete);
        ea.GetEvent<Common.Events.LoginEvent>().Subscribe(OnUserLogin);
        TodoLists.CollectionChanged += TodoListsOnCollectionChanged;
    }

    private void TodoListsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action is NotifyCollectionChangedAction.Remove or NotifyCollectionChangedAction.Move)
        {
            for (var i = 0; i < TodoLists.Count; i++)
            {
                TodoListBindable item = TodoLists[i];
                item.TodoList.Order = i;
            }
            context.SaveChanges();
        }
    }

    public ObservableCollection<TodoListBindable> TodoLists { get; } = new();

    public void CreateSection(string sectionName)
    {
        var list = new TodoList();
        list.Name = sectionName;
        list.User = GetCurrentUser();
        list.Order = TodoLists.Count;
        context.Lists.Add(list);
        context.SaveChanges();

        var todoList = new TodoListBindable(ea, list, context);
        todoList.Title = sectionName;
        TodoLists.Add(todoList);
    }

    private void OnToDoDelete(TodoItemBindable value)
    {
        foreach (TodoListBindable list in TodoLists)
        {
            list.Items.Remove(value);
        }
    }

    private void OnUserLogin(LoginModel model)
    {
        loginUser = model;
        TodoLists.Clear();

        List<TodoList> currentUsersLists = GetCurrentUserLists();

        foreach (TodoList list in currentUsersLists)
        {
            var todos = new List<TodoItemBindable>();

            foreach (Todo todo in context.Todos.Where(o => o.TodoList.TodoListId == list.TodoListId))
            {
                var bindable = new TodoItemBindable(ea, todo, context)
                {
                    Text = todo.Name,
                    IsDone = todo.IsDone
                };
                todos.Add(bindable);
            }

            var todoList = new TodoListBindable(ea, list, context, todos)
            {
                Title = list.Name
            };

            TodoLists.Add(todoList);
        }
    }

    private List<TodoList> GetCurrentUserLists()
    {
        List<TodoList> todoLists = context
                                   .Lists
                                   .Where(o => o.User.Username == loginUser.Username)
                                   .ToList();
        todoLists.Sort(TodoList.OrderComparison);
        return todoLists;
    }

    

    private User GetCurrentUser()
    {
        return context.Users.First(o => o.Username == loginUser.Username);
    }
}