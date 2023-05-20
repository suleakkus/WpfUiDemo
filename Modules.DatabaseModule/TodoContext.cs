using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Modules.DatabaseModule;

public class TodoContext : DbContext
{
    public TodoContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        string path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "todo.db");
    }
    
    public DbSet<User> Users { get; set; }

    public string DbPath { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlite($"Data Source={DbPath}");
    }
}