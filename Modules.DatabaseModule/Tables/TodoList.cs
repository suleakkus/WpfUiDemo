using System.Collections.Generic;

namespace Modules.DatabaseModule.Tables;

public class TodoList : BaseEntity
{
    public string Name { get; set; }
    public User User { get; set; }
    public int TodoListId { get; set; }
    public int Order { get; set; }
    public List<Todo> Todos { get; } = new();
}