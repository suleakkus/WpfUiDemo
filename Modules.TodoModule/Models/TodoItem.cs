using Modules.TodoModule.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Modules.TodoModule.Models;

public class TodoItem : BindableBase
{
    private readonly IEventAggregator ea;
    private bool isDone;
    private string text;

    public TodoItem(IEventAggregator ea)
    {
        this.ea = ea;
        text = string.Empty;
    }

    public bool IsDone
    {
        get => isDone;
        set => SetProperty(ref isDone, value);
    }

    public string Text
    {
        get => text;
        set => SetProperty(ref text, value);
    }

    public DelegateCommand FinishCommand => new(OnFinish);

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
}