using Modules.DatabaseModule;
using Modules.DatabaseModule.Tables;
using Modules.TodoModule.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Modules.TodoModule.Models;

public class TodoItemBindable : BindableBase
{
    private readonly TodoContext context;
    private readonly IEventAggregator ea;
    private bool isDone;
    private string text;

    public TodoItemBindable(IEventAggregator ea, Todo todo, TodoContext context)
    {
        this.ea = ea;
        this.context = context;
        Todo = todo;
        text = string.Empty;
    }

    public bool IsDone
    {
        get => isDone;
        set => SetProperty(ref isDone, value, UpdateDb);
    }

    public string Text
    {
        get => text;
        set => SetProperty(ref text, value, UpdateDb);
    }

    public DelegateCommand FinishCommand => new(OnFinish);

    public DelegateCommand DeleteCommand => new(ToDoDelete);

    public Todo Todo { get; }
    
    private void UpdateDb()
    {
        Todo.IsDone = IsDone;
        Todo.Name = Text;
        context.Todos.Update(Todo);
        context.SaveChangesAsync();
    }

    private void OnFinish()
    {
        if (IsDone)
        {
            IsDone = false;
            ea.GetEvent<TodoEvents.TodoItemUnFinishedEvent>().Publish(this);
        }
        else
        {
            IsDone = true;
            ea.GetEvent<TodoEvents.TodoItemFinishedEvent>().Publish(this);
        }
    }

    private void ToDoDelete()
    {
        context.Todos.Remove(Todo);
        context.SaveChangesAsync();
        ea.GetEvent<TodoEvents.TodoItemDeleteEvent>().Publish(this);
    }
}