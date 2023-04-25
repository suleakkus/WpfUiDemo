using Modules.TodoModule.Models;
using Prism.Events;

namespace Modules.TodoModule.Events;

public static class TodoEvents
{
    public class TodoItemFinishedEvent : PubSubEvent<TodoItem> { }
    public class TodoItemUnFinishedEvent : PubSubEvent<TodoItem> { }

    public class TodoItemDeleteEvent:PubSubEvent<TodoItem>{}
}