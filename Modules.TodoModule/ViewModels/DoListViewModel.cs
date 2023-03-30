using System.Collections.ObjectModel;
using JetBrains.Annotations;
using Modules.TodoModule.Events;
using Modules.TodoModule.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Modules.TodoModule.ViewModels;

[UsedImplicitly]
public class DoListViewModel : BindableBase
{
    private readonly IEventAggregator ea;
    private readonly TodoList dones;
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
    }

    public ObservableCollection<TodoList> TodoLists { get; }

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

    public void CreateSection(string sectionName)
    {
        var todoList = new TodoList(ea);
        todoList.Title = sectionName;
        TodoLists.Add(todoList);
    }
}