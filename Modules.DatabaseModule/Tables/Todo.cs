namespace Modules.DatabaseModule.Tables;

public class Todo : BaseEntity
{
    public int TodoId { get; set; }
    public string Name { get; set; }
    public TodoList TodoList { get; set; }
    public bool IsDone { get; set; }
    public int Order { get; set; }
}