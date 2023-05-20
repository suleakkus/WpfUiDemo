using System;
using System.Linq;
using Modules.DatabaseModule;
using Modules.DatabaseModule.Tables;

namespace SandBoxDatabaseConsole;

public static class Program
{
    public static void Main()
    {
        var context = new TodoContext();

        //AddMilkaAndList(context);

        context.Todos.Add(
            new Todo
            {
                Name = "Todo 2",
                Order = 0,
                IsDone = false,
                TodoList = context.Lists.First()
            });
        context.SaveChanges();
        
    }

    private static void AddMilkaAndList(TodoContext context)
    {
        context.Users.Add(
            new User
            {
                Name = "milka",
                Password = "123",
                Surname = "lion",
                Username = "milka"
            });

        context.SaveChanges();

        foreach (User user in context.Users)
        {
            Console.WriteLine(user.UserId);
        }

        var todoList = new TodoList
        {
            User = context.Users.First(),
            Name = "My Todo List",
            Order = 0
        };

        context.Lists.Add(todoList);

        context.SaveChanges();
    }

    private static void BloggingDemos()
    {
        using var db = new BloggingContext();

        // Note: This sample requires the database to be created before running.
        Console.WriteLine($"Database path: {db.DbPath}.");

        // Create
        Console.WriteLine("Inserting a new blog");
        db.Add(
            new Blog
            {
                Url = "http://blogs.msdn.com/adonet"
            });
        db.SaveChanges();

        // Read
        Console.WriteLine("Querying for a blog");
        Blog blog = db.Blogs
                      .OrderBy(b => b.BlogId)
                      .First();

        // Update
        Console.WriteLine("Updating the blog and adding a post");
        blog.Url = "https://devblogs.microsoft.com/dotnet";
        for (var i = 0; i < 100; i++)
        {
            blog.Posts.Add(
                new Post
                {
                    Title = $"Hello World{i + 1}",
                    Content = $"I wrote an app using EF Core! {i + 1}"
                });
        }

        db.SaveChanges();

        // Delete
        Console.WriteLine("Delete the blog");
        //db.Remove(blog);
        db.SaveChanges();
    }
}