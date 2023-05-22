using Modules.TodoModule.Models;
using Prism.Events;

namespace Modules.TodoModule.Events;

public static class TodoEvents
{
    public class TodoItemFinishedEvent : PubSubEvent<TodoItemBindable> { }
    public class TodoItemUnFinishedEvent : PubSubEvent<TodoItemBindable> { }

    public class TodoItemDeleteEvent:PubSubEvent<TodoItemBindable>{}
}