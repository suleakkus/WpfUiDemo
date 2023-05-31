using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            var todoList = new TodoListBindable(ea, list, context)
            {
                Title = list.Name
            };

            foreach (Todo todo in context.Todos.Where(o => o.TodoList.TodoListId == list.TodoListId))
            {
                todoList.Items.Add(
                    new TodoItemBindable(ea, todo, context)
                    {
                        Text = todo.Name,
                        IsDone = todo.IsDone
                    });
            }

            TodoLists.Add(todoList);
        }
    }

    private List<TodoList> GetCurrentUserLists()
    {
        List<TodoList> todoLists = context
                                   .Lists
                                   .Where(o => o.User.Username == loginUser.Username)
                                   .ToList();
        todoLists.Sort(TodoListOrderComparison);
        return todoLists;
    }

    private static int TodoListOrderComparison(TodoList x, TodoList y)
    {
        if (x.Order < y.Order)
        {
            return x.Order;
        }

        return y.Order;
    }

    private User GetCurrentUser()
    {
        return context.Users.First(o => o.Username == loginUser.Username);
    }
}