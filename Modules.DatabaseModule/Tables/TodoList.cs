namespace Modules.DatabaseModule.Tables;

public class TodoList : BaseEntity
{
    public TodoList()
    {
        Name = string.Empty;
    }

    public string Name { get; set; }
    public User User { get; set; }
    public int TodoListId { get; init; }
    public int Order { get; set; }

    public static int OrderComparison(TodoList x, TodoList y)
    {
        if (x.Order < y.Order)
        {
            return x.Order;
        }

        return y.Order;
    }
}