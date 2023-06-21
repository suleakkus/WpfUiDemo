using System;
using Modules.TodoModule.Models;

namespace Modules.TodoModule.Controllers;

public static class TodoColorSelector
{
    public static string SelectBackgroundColor(TodoItemBindable todo)
    {
        if (todo.IsDone)
        {
            return "#228B22";
        }

        if (todo.DueDate != null && todo.DueDate < DateTime.Now)
        {
            return "#ff3c00";
        }

        return "#2F2F2F";
    }
}