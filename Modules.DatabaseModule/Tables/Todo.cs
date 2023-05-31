namespace Modules.DatabaseModule.Tables;

public class Todo : BaseEntity
{
    public int TodoId { get; set; }
    public string Name { get; set; }
    public TodoList TodoList { get; set; }
    public bool IsDone { get; set; }
    public int Order { get; set; }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((Todo)obj);
    }

    public override int GetHashCode()
    {
        return TodoId;
    }

    private bool Equals(Todo other)
    {
        return TodoId == other.TodoId;
    }
}